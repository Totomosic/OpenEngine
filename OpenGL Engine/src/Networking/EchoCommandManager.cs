using System;
using System.Collections.Generic;

namespace OpenEngine
{
    public class EchoCommandManager
    {

        #region FIELDS

        private Dictionary<string, Func<SocketData, string>> commandDict;
        private GameServer server;

        #endregion

        #region CONSTRUCTORS

        public EchoCommandManager(GameServer serv)
        {
            server = serv;
            commandDict = new Dictionary<string, Func<SocketData, string>>();
        }

        #endregion

        #region PROPERTIES

        public virtual Dictionary<string, Func<SocketData, string>> Dictionary
        {
            get { return commandDict; }
            set { commandDict = value; }
        }

        #endregion

        #region PUBLIC METHODS

        public virtual void AddDefaultCommands()
        {
            AddCommand("GetName", GetNameFunction);
            AddCommand("GetTotalConnections", GetConnectionsFunction);
            AddCommand("GetPort", GetPortFunction);
            AddCommand("GetWirelessIP", GetWirelessIPFunction);
            AddCommand("GetEthernetIP", GetEthernetIPFunction);
        }

        public virtual bool HasCommand(string command)
        {
            return Dictionary.ContainsKey(command);
        }

        public virtual Func<SocketData, string> GetCommand(string command)
        {
            return Dictionary[command];
        }

        public virtual string Execute(SocketData data, string command)
        {
            return Dictionary[command](data);
        }

        public virtual void AddCommand(string command, Func<SocketData, string> executable)
        {
            commandDict.Add(command, executable);
        }

        public virtual void RemoveCommand(string command)
        {
            commandDict.Remove(command);
        }

        #endregion

        #region PRIVATE METHODS

        private string GetNameFunction(SocketData data)
        {
            return data.Name;
        }

        private string GetConnectionsFunction(SocketData data)
        {
            return server.TotalConnections.ToString();
        }

        private string GetPortFunction(SocketData data)
        {
            return server.Port.ToString();
        }

        private string GetWirelessIPFunction(SocketData data)
        {
            return server.WirelessIP;
        }

        private string GetEthernetIPFunction(SocketData data)
        {
            return server.EthernetIP;
        }

        #endregion

    }
}
