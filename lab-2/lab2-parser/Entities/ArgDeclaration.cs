namespace Lab2
{
    public class ArgDeclaration
    {
        private string _type;
        private string _name;
        private bool _haveRequestBody;

        public ArgDeclaration(string Type, string Name, bool HaveRequestBody)
        {
            _type = Type;
            _name = Name;
            _haveRequestBody = HaveRequestBody;
        }

        public string GetType()
        {
            return _type;
        }

        public string GetName()
        {
            return _name;
        }
        public bool GetHaveRequestBody()
        {
            return _haveRequestBody;
        }
    }
}