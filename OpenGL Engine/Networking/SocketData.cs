using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace OpenEngine
{
    public class SocketData
    {

        private Socket socket;
        private string name;

        public SocketData(string sockName, Socket sock)
        {
            socket = sock;
            name = sockName;
        }

        public Socket Socket
        {
            get { return socket; }
        }

        public string Name
        {
            get { return name; }
        }

    }
}
