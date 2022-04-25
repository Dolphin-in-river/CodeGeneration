using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Lab2
{
    public class ParserDTO
    {
        public const string PathToDTO =
            @"C:\Users\Public\Techs\src\main\java\com\example\test1\entities";

        public string[] _pathToEntity;
        public List<DTODeclarationField> declarationsField = new List<DTODeclarationField>();
        public List<DTODeclaration> declarationsDTO = new List<DTODeclaration>();
        public List<ArgDeclaration> args = new List<ArgDeclaration>();
        public void Do()
        {
            string regexPost = @"^\s{4}(?<type>\S*)\s+(?<typeReturn>\S*)\s+(?<name>\w+);$";
            _pathToEntity = Directory.GetFiles(PathToDTO);
            foreach (var item in _pathToEntity)
            {
                string className = item.Remove(item.Length - 5).Substring(PathToDTO.Length + 1);
                foreach (string line in File.ReadLines(item))
                {
                    if (Regex.IsMatch(line, regexPost))
                    {
                        AddToDeclaration(regexPost, line);
                    }

                    if (line.Contains($@" {className}("))
                    {
                        var currentlyArgs = line.Remove(line.Length - 3).Substring(12 + className.Length);
                        string currently = "";
                        string nameArg = "";
                        string typeArg = "";
                        for (int i = 0; i < currentlyArgs.Length; i++)
                        {
                            if (!currentlyArgs[i].Equals(' '))
                            {
                                if (!currentlyArgs[i].Equals(','))
                                {
                                    currently += currentlyArgs[i];
                                }

                                if (i == currentlyArgs.Length - 1 || currentlyArgs[i].Equals(','))
                                {
                                    nameArg = currently;
                                    var arguments = new ArgDeclaration(typeArg, nameArg, false);
                                    args.Add(arguments);
                                }
                            }
                            else
                            {
                                typeArg = currently;
                                if (typeArg == "String")
                                {
                                    typeArg = "string";
                                }
                                currently = "";
                            }
                        }
                    }
                }
                
                declarationsDTO.Add(new DTODeclaration(declarationsField, className, args));
                declarationsField = new List<DTODeclarationField>();
                args = new List<ArgDeclaration>();
            }
        }

        private void AddToDeclaration(string regexPost, string line)
        {
            MatchCollection matches = Regex.Matches(line, regexPost);
            foreach (Match match in matches)
            {
                Group typeGroup = match.Groups["type"];
                Group typeReturnGroup = match.Groups["typeReturn"];
                Group nameGroup = match.Groups["name"];
                string type = typeGroup.Value;
                string typeReturn = typeReturnGroup.Value;
                string name = nameGroup.Value;
                if (typeReturn == "String")
                {
                    typeReturn = "string";
                }
                if (typeReturn != "return")
                {
                    /*
                    Console.Write(type);
                    Console.Write(" ");
                    Console.Write(typeReturn);
                    Console.Write(" ");
                    Console.Write(name);
                    Console.WriteLine("");
                    */
                    declarationsField.Add(new DTODeclarationField(type, typeReturn, name));
                }
            }
        }
    }
}