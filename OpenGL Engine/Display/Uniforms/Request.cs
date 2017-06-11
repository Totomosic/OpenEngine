using System;
using System.Collections.Generic;

namespace OpenEngine
{
    public abstract class Request
    {

        public abstract string VariableName
        {
            get;
            set;
        }

        public abstract void UploadValue(ShaderProgram program);

    }

    public class Request<T> : Request
        where T : struct
    {

        #region FIELDS

        private string varname;
        private T value;

        #endregion

        #region CONSTRUCTORS

        public Request(string vName, T val)
        {
            varname = vName;
            value = val;
        }

        #endregion

        #region PROPERTIES

        public T Value
        {
            get { return value; }
        }

        public override string VariableName
        {
            get { return varname; }
            set { varname = value; }
        }

        #endregion

        #region PUBLIC METHODS

        public override void UploadValue(ShaderProgram program)
        {
            program.SetUniformValue(varname, value);
        }

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
