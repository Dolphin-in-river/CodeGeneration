namespace Lab2
{
    public class DTODeclarationField
    {
        private string _type;
        private string _returnType;
        private string _name;

        public DTODeclarationField(string type, string returnType, string name)
        {
            _name = name;
            _type = type;
            _returnType = returnType;
        }

        public string GetType()
        {
            return _type;
        }
        public string GetReturnType()
        {
            return _returnType;
        }
        public string GetName()
        {
            return _name;
        }
    }
}