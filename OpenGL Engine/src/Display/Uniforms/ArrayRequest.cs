using System;


namespace OpenEngine
{
    public class ArrayRequest<T> : Request
        where T : struct
    {
        #region FIELDS

        private string varname;
        private T[] value;

        #endregion

        #region CONSTRUCTORS

        public ArrayRequest(string vName, T[] val)
        {
            varname = vName;
            value = val;
        }

        #endregion

        #region PROPERTIES

        public T[] Value
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
