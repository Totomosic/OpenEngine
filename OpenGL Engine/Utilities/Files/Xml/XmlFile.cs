using System;
using System.Collections.Generic;
using System.Xml;

namespace OpenEngine
{
    public class XmlFile : DataFile
    {

        #region FIELDS

        private XmlDocument document;
        private XmlNode rootNode;

        #endregion

        #region CONSTRUCTORS

        public XmlFile(string path) : base(path)
        {
            CreateFile();
        }

        #endregion

        #region PROPERTIES

        public XmlDocument Document
        {
            get { return document; }
            set { document = value; }
        }

        public XmlNode RootNode
        {
            get { return rootNode; }
            set { rootNode = value; }
        }

        #endregion

        #region PUBLIC METHODS

        public XmlNodeList GetNodes(string path)
        {
            return RootNode.SelectNodes(path);
        }

        public XmlNode GetNode(string path)
        {
            return RootNode.SelectSingleNode(path);
        }

        #endregion

        #region PRIVATE METHODS

        private void CreateFile()
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(Path);
            document = xml;
            rootNode = document.DocumentElement;
        }

        #endregion

    }
}
