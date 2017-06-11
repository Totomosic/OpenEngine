using System;
using System.IO;

namespace OpenEngine
{
    public class DataFile
    {

        #region FIELDS

        private string filePath;

        #endregion

        #region CONSTRUCTORS

        public DataFile(string path)
        {
            filePath = path;
        }

        #endregion

        #region PROPERTIES

        public virtual string Path
        {
            get { return filePath; }
            set { filePath = value; }
        }

        #endregion

        #region PUBLIC METHODS

        public virtual StreamReader GetStream()
        {
            return new StreamReader(Path);
        }

        public virtual string GetExtension(bool includeDot = true)
        {
            int index = Path.LastIndexOf(".");
            return Path.Substring(index + ((includeDot) ? 0 : 1));
        }

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
