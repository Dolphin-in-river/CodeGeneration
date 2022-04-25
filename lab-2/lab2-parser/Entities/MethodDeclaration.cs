using System.Collections.Generic;

namespace Lab2
{
    public class MethodDeclaration
    {
        private string _methodName;
        private string _returnType;
        private List<ArgDeclaration> _argList = new List<ArgDeclaration>();
        private string _url;
        private string _httpMethodName;

        public MethodDeclaration(string MethodName, string ReturnType, List<ArgDeclaration> ArgList, string Url, string HttpMethodName)
        {
            _url = Url;
            _methodName = MethodName;
            _argList = ArgList;
            _returnType = ReturnType;
            _httpMethodName = HttpMethodName;
        }

        public string GetMethodName()
        {
            return _methodName;
        }
        public string GetReturnType()
        {
            return _returnType;
        }
        public List<ArgDeclaration> GetArgList()
        {
            return _argList;
        }
        public string GetUrl()
        {
            return _url;
        }
        public string GetHttpMethodName()
        {
            return _httpMethodName;
        }

        public string GetNameArgWithRequestBody()
        {
            foreach (var item in _argList)
            {
                if (item.GetHaveRequestBody())
                {
                    return item.GetName();
                }
            }

            return null;
        }
    }
}