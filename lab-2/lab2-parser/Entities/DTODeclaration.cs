using System.Collections.Generic;

namespace Lab2
{
    public class DTODeclaration
    {
        public List<DTODeclarationField> Fields
        {
            get;
        }

        public List<ArgDeclaration> Args
        {
            get;
        }

        public string ClassName
        {
            get;
        }

        public DTODeclaration(List<DTODeclarationField> fields, string className, List<ArgDeclaration> args)
        {
            Fields = fields;
            ClassName = className;
            Args = args;
        }
    }
}