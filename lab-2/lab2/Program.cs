using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            GenerateClient();
        }

        public static void GenerateClient()
        {
            var parser = new ParserController();
            parser.Do();

            var parserDTO = new ParserDTO();
            parserDTO.Do();

            var generator = new DTOGenerator();
            generator.Do(parserDTO);

            var clientGenerator = new ClientGenerator();
            clientGenerator.Do(parser);
        }

        public void SomeTests()
        {/*
            var client = new Client();
            Master firstMaster = new Master("Ivan", "26-02-2003");

            Master secondMaster = new Master("Danya", "27-02-2003");

            var response1 = client.CreateMaster(firstMaster);
            Thread.Sleep(200);
            var response2 = client.CreateMaster(secondMaster);
            Thread.Sleep(200);
            foreach (var item in client.GetMasters().Result)
            {
                Console.WriteLine(item.name);
            }


            Cat newCat = new Cat("Cat2", "RussianBlue", "26-20=2020", "Black", 30);
            Cat newCat2 = new Cat("Cat3", "RussianBlue", "26-20=2020", "Black", 98);
            //var response = client.CreateCat(newCat);
            //Console.WriteLine(response.Result);
            var response3 = client.SetFriends(new List<Cat>() {newCat2}, 297);
            Console.WriteLine(response3.Result);*/
        }
    }
}