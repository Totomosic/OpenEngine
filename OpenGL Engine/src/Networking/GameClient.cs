using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace OpenEngine
{
    public class GameClient
    {

        #region FIELDS

        private string name;
        private int port;
        private Socket clientSocket;
        private byte[] dataBuffer;

        private Action<string> receiveDataCallback;

        #endregion

        #region CONSTRUCTORS

        public GameClient(string socketName, int clientPort)
        {
            name = socketName;
            port = clientPort;
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            dataBuffer = new byte[clientSocket.ReceiveBufferSize];
            ReceiveDataCallback = ReceiveData;
        }

        #endregion

        #region PROPERTIES

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Port
        {
            get { return port; }
            protected set { port = value; }
        }

        public Socket Socket
        {
            get { return clientSocket; }
            protected set { clientSocket = value; }
        }

        public Action<string> ReceiveDataCallback
        {
            get { return receiveDataCallback; }
            set { receiveDataCallback = value; }
        }

        #endregion

        #region PUBLIC METHODS

        public void StartReceiving()
        {
            clientSocket.BeginReceive(dataBuffer, 0, dataBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
        }

        public void TryConnect(IPEndPoint endpoint)
        {
            while (!Socket.Connected)
            {
                Connect(endpoint);
            }
        }

        public void Connect(IPEndPoint endpoint)
        {
            try
            {
                Socket.Connect(endpoint);
                Socket.Send(Encoding.ASCII.GetBytes(Name));
            }
            catch (SocketException)
            {
                Console.WriteLine("Failed to connect");
            }
        }

        public void SendText(string text)
        {
            byte[] data = Encoding.ASCII.GetBytes(text);
            Socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);
        }

        #endregion

        #region PRIVATE METHODS

        private void ReceiveCallback(IAsyncResult ar)
        {
            int received = clientSocket.EndReceive(ar);
            Array.Resize(ref dataBuffer, received);
            ReceiveDataCallback(Encoding.ASCII.GetString(dataBuffer));
            Array.Resize(ref dataBuffer, clientSocket.ReceiveBufferSize);
            StartReceiving();
        }

        private void SendCallback(IAsyncResult ar)
        {
            Socket.EndSend(ar);
        }

        private void ReceiveData(string data)
        {
            Console.WriteLine("Received: " + data);
        }

        #endregion

    }
}
