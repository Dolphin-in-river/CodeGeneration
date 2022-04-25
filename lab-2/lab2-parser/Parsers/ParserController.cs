using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Lab2
{
    public class ParserController
    {
        public const string PathToController =
            @"C:\Users\Public\Techs\src\main\java\com\example\test1\controller\Controller.java";

        private List<MethodDeclaration> _methods = new List<MethodDeclaration>();

        public void Do()
        {
            int counter = 0;
            string regexPost = @"(?<type>\S+)\S*(value|path){1}\s+=\s+(?<url>\S+)\w*";
            string regex =
                @"(public|private){1}\s+(?<returntype>\S+)\s+(?<nameMethod>\w+)\S?(?<args>.{1,})\S+";
            bool flag = false;

            string returnType = "";
            string nameMethod = "";
            string args = "";
            string url = "";
            string type = "";
            bool haveRequestBody = false;
            foreach (string line in System.IO.File.ReadLines(PathToController))
            {
                if (flag)
                {
                    flag = false;
                    MatchCollection matches = Regex.Matches(line, regex);
                    foreach (Match match in matches)
                    {
                        Group typeGroup = match.Groups["returntype"];
                        returnType = typeGroup.Value.Remove(typeGroup.Value.Length);
                        Group nameMethodGroup = match.Groups["nameMethod"];
                        nameMethod = nameMethodGroup.Value;
                        Group typeG = match.Groups["args"];
                        args = typeG.Value.Remove(typeG.Value.Length - 1);
                    }

                    var listArgs = new List<ArgDeclaration>();
                    string currently = "";
                    string nameArg = "";
                    string typeArg = "";
                    for (int i = 0; i < args.Length; i++)
                    {
                        if (!args[i].Equals(' '))
                        {
                            if (!args[i].Equals(','))
                            {
                                currently += args[i];
                            }

                            if (i == args.Length - 1 || args[i].Equals(','))
                            {
                                nameArg = currently;
                                var arguments = new ArgDeclaration(typeArg, nameArg, haveRequestBody);
                                /*
                                Console.Write(typeArg);
                                Console.Write(" ");
                                Console.Write(nameArg);
                                Console.Write(" ");
                                Console.Write(haveRequestBody);
                                Console.WriteLine(" ");
                                */
                                listArgs.Add(arguments);
                                haveRequestBody = false;
                            }
                        }
                        else
                        {
                            if (currently.Equals("@RequestBody"))
                            {
                                haveRequestBody = true;
                            }
                            else
                            {
                                typeArg = currently;
                            }

                            currently = "";
                        }
                    }

                    type = type.Remove(type.Length - 7);
                    returnType = returnType.Remove(returnType.Length - 1).Substring(15);
                    var buff = new MethodDeclaration(nameMethod, returnType, listArgs, url, type);
/*
                    Console.Write(type);
                    Console.Write(" ");
                    Console.Write(url);
                    Console.Write(" ");
                    Console.Write(returnType);
                    Console.Write(" ");
                    Console.Write(nameMethod);
                    Console.Write(" ");
                    Console.WriteLine();
*/
                    _methods.Add(buff);
                }

                if (Regex.IsMatch(line, regexPost))
                {
                    flag = true;
                    haveRequestBody = false;
                    MatchCollection matches = Regex.Matches(line, regexPost);
                    foreach (Match match in matches)
                    {
                        Group urlGroup = match.Groups["url"];
                        Group typeGroup = match.Groups["type"];
                        url = urlGroup.Value.Remove(urlGroup.Value.Length - 2).Substring(1);
                        type = typeGroup.Value.Remove(typeGroup.Value.Length - 1).Substring(1);
                    }
                }
            }
        }

        public List<MethodDeclaration> GetMethods()
        {
            return _methods;
        }
    }
}