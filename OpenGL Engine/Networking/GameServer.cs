using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace OpenEngine
{
    public class GameServer
    {

        #region FIELDS

        private string name;
        private int port;
        private Socket serverSocket;
        private int backlog;
        private bool setup;

        private Dictionary<string, Socket> clients;
        private Dictionary<string, byte[]> buffers;

        private bool useCommands;
        private EchoCommandManager commands;

        //Callbacks
        private Action<SocketData> newConnectionCallback;
        private Action<SocketData, string, bool> receiveDataCallback;

        #endregion

        #region CONSTRUCTORS

        public GameServer(string serverName, int serverPort, int bcklog = 50, bool start = true, bool enableCommands = true)
        {
            name = serverName;
            port = serverPort;
            backlog = bcklog;
            setup = false;
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clients = new Dictionary<string, Socket>();
            buffers = new Dictionary<string, byte[]>();
            useCommands = enableCommands;

            //Callbacks
            ReceiveDataCallback = ReceiveData;
            NewConnectionCallback = delegate { };

            commands = new EchoCommandManager(this);

            if (start)
            {
                Start();
            }

        }

        #endregion

        #region PROPERTIES

        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        public virtual bool Ready
        {
            get { return setup; }
            protected set { setup = value; }
        }

        public virtual int Port
        {
            get { return port; }
            protected set { port = value; }
        }

        public virtual int Backlog
        {
            get { return backlog; }
            protected set { backlog = value; }
        }

        public virtual Socket Socket
        {
            get { return serverSocket; }
        }

        public virtual EchoCommandManager CommandManager
        {
            get { return commands; }
            set { commands = value; }
        }

        public virtual bool UseCommands
        {
            get { return useCommands; }
            set { useCommands = value; }
        }

        public virtual Action<SocketData, string, bool> ReceiveDataCallback
        {
            get { return receiveDataCallback; }
            set { receiveDataCallback = value; }
        }

        public virtual Action<SocketData> NewConnectionCallback
        {
            get { return newConnectionCallback; }
            set { newConnectionCallback = value; }
        }

        public virtual int TotalConnections
        {
            get { return clients.Count; }
        }

        public string WirelessIP
        {
            get;
            set;
        }

        public string EthernetIP
        {
            get;
            set;
        }

        #endregion

        #region PUBLIC METHODS

        public virtual void Start()
        {
            WirelessIP = GetLocalIPv4(NetworkInterfaceType.Wireless80211);
            EthernetIP = GetLocalIPv4(NetworkInterfaceType.Ethernet);
            Socket.Bind(new IPEndPoint(IPAddress.Any, Port));
            Socket.Listen(Backlog);
            Socket.BeginAccept(new AsyncCallback(AcceptCallback), null);
            Ready = true;
        }

        public virtual bool HasClientName(string name)
        {
            return clients.ContainsKey(name);
        }

        public virtual string[] GetClientNames()
        {
            List<string> keys = new List<string>();
            foreach (string key in clients.Keys)
            {
                keys.Add(key);
            }
            return keys.ToArray();
        }

        public virtual void SendText(string clientName, string text)
        {
            SendText(clients[clientName], clientName, text);
        }

        public virtual void SendText(Socket socket, string socketName, string text)
        {
            byte[] textData = Encoding.ASCII.GetBytes(text);
            socket.BeginSend(textData, 0, textData.Length, SocketFlags.None, new AsyncCallback(SendCallback), new SocketData(socketName, socket));
        }

        public virtual void SendTextToAll(string text)
        {
            foreach (string key in clients.Keys)
            {
                SendText(key, text);
            }
        }

        public virtual void SendTextToAll(string text, List<Socket> exceptions)
        {
            foreach (string key in clients.Keys)
            {
                if (!exceptions.Contains(clients[key]))
                {
                    SendText(key, text);
                }
            }
        }

        #endregion

        #region PRIVATE METHODS

        private void AcceptCallback(IAsyncResult ar)
        {
            // new connection received
            Socket socket = Socket.EndAccept(ar);
            byte[] data = new byte[socket.ReceiveBufferSize];
            int received = socket.Receive(data);
            Array.Resize(ref data, received);
            string socketName = Encoding.ASCII.GetString(data);
            socketName = CheckName(socketName);
            SocketData socketData = new SocketData(socketName, socket);

            NewConnectionCallback(socketData);

            clients.Add(socketName, socket);
            buffers.Add(socketName, new byte[socket.ReceiveBufferSize]);
            socket.BeginReceive(buffers[socketName], 0, buffers[socketName].Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socketData);
            Socket.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            SocketData sock = (SocketData)ar.AsyncState;
            Socket socket = sock.Socket;
            int received = 0;
            try
            {
                received = socket.EndReceive(ar);
                byte[] buffer = buffers[sock.Name];
                Array.Resize(ref buffer, received);
                string text = Encoding.ASCII.GetString(buffer);
                bool isCommand = TestCommand(sock, text);
                ReceiveDataCallback(sock, text, isCommand);                
                Array.Resize(ref buffer, socket.ReceiveBufferSize);
                buffers[sock.Name] = buffer;
                socket.BeginReceive(buffers[sock.Name], 0, buffers[sock.Name].Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), sock);
            }
            catch (SocketException)
            {
                Console.WriteLine("Connection was closed, removing connection: " + sock.Name);
                sock.Socket.Close();
                clients.Remove(sock.Name);
                buffers.Remove(sock.Name);
            }            
        }

        private void SendCallback(IAsyncResult ar)
        {
            SocketData sock = (SocketData)ar.AsyncState;
            try
            {
                sock.Socket.EndSend(ar);
            }
            catch (SocketException)
            {

            }
       }

        private string CheckName(string name, int beginCount = 0)
        {
            int count = beginCount;
            string newName = name;
            if (clients.ContainsKey(name))
            {
                newName = name + count.ToString();
                count++;
                if (clients.ContainsKey(newName))
                {
                    newName = CheckName(newName, count);
                }
            }
            return newName;
        }

        private void ReceiveData(SocketData socketData, string text, bool isCommand)
        {
            Console.WriteLine(text);
        }

        private bool TestCommand(SocketData socketData, string text)
        {
            if (CommandManager.HasCommand(text) && UseCommands)
            {
                string value = CommandManager.Execute(socketData, text);
                SendText(socketData.Socket, socketData.Name, value);
                return true;
            }
            return false;
        }

        private string GetLocalIPv4(NetworkInterfaceType _type)
        {
            string output = "";
            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.NetworkInterfaceType == _type && item.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            output = ip.Address.ToString();
                        }
                    }
                }
            }
            return output;
        }

        #endregion

    }
}
