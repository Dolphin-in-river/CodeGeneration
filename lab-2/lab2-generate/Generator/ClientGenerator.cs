using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Lab2
{
    public class ClientGenerator
    {
        public async Task Do(ParserController controller)
        {
            //-------keywords-------
            SyntaxTokenList publicKey = SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword));
            SyntaxTokenList asyncToken = SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.AsyncKeyword));
            //-------usings-------

            var using1 = SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName("System.Collections.Generic"));
            var using2 = SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName("System.Net.Http"));
            var using3 = SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName("System.Threading.Tasks"));
            var using4 = SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName("Newtonsoft.Json"));
            var using5 = SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName("System.Text"));
            var usings = new List<UsingDirectiveSyntax>()
            {
                using1, using2, using3, using4, using5
            };
            var usingList = SyntaxFactory.List(usings);

            //-------namespace-------
            var namespaceDeclaration = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.IdentifierName("Lab2"));

            //-------classname-------

            var webClass = SyntaxFactory.ClassDeclaration("Client").WithModifiers(publicKey);

            //-------fields with getters and setters-------
            var resultMethods = new List<MethodDeclarationSyntax>();
            foreach (var method in controller.GetMethods())
            {
                string returnType = "";
                if (method.GetHttpMethodName() == "Get")
                {
                    returnType = method.GetReturnType();
                }

                if (method.GetHttpMethodName() == "Post")
                {
                    returnType = "HttpResponseMessage";
                }

                int length = Math.Max(method.GetArgList().Count * 2 - 1, 0);
                var arguments = new SyntaxNodeOrToken[length];
                int counter = 0;
                for (int i = 0; i < length; i++)
                {
                    if (i % 2 == 0)
                    {
                        var currentlyArgument = SyntaxFactory
                            .Parameter(SyntaxFactory.Identifier(method.GetArgList()[counter].GetName()))
                            .WithType(
                                SyntaxFactory.IdentifierName(method.GetArgList()[counter].GetType()));

                        arguments[i] = currentlyArgument;
                        counter++;
                    }

                    if (i % 2 == 1)
                    {
                        arguments[i] = SyntaxFactory.Token(SyntaxKind.CommaToken);
                    }
                }

                MethodDeclarationSyntax resultMethod = null;
                if (method.GetHttpMethodName() == "Get")
                {
                    resultMethod = SyntaxFactory.MethodDeclaration(
                            SyntaxFactory.IdentifierName("Task<" + returnType + ">"),
                            SyntaxFactory.Identifier(method.GetMethodName()))
                        .WithParameterList(
                            SyntaxFactory.ParameterList(
                                SyntaxFactory.SeparatedList<ParameterSyntax>(arguments)))
                        .WithModifiers(
                            SyntaxFactory.TokenList(
                                new[]
                                {
                                    SyntaxFactory.Token(SyntaxKind.PublicKeyword),
                                    SyntaxFactory.Token(SyntaxKind.AsyncKeyword)
                                }))
                        .WithBody(
                            SyntaxFactory.Block(SyntaxFactory.SingletonList<StatementSyntax>(
                                SyntaxFactory.ReturnStatement(SyntaxFactory.InvocationExpression(
                                        SyntaxFactory.MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression,
                                            SyntaxFactory.IdentifierName("JsonConvert"),
                                            SyntaxFactory.GenericName(
                                                    SyntaxFactory.Identifier("DeserializeObject"))
                                                .WithTypeArgumentList(
                                                    SyntaxFactory.TypeArgumentList(
                                                        SyntaxFactory.SingletonSeparatedList<TypeSyntax>(
                                                            SyntaxFactory.IdentifierName(
                                                                returnType))))))
                                    .WithArgumentList(
                                        SyntaxFactory.ArgumentList(
                                            SyntaxFactory.SingletonSeparatedList<ArgumentSyntax>(
                                                SyntaxFactory.Argument(
                                                    SyntaxFactory.AwaitExpression(
                                                        SyntaxFactory.InvocationExpression(
                                                                SyntaxFactory.MemberAccessExpression(
                                                                    SyntaxKind.SimpleMemberAccessExpression,
                                                                    SyntaxFactory.ObjectCreationExpression(
                                                                            SyntaxFactory.IdentifierName(
                                                                                "HttpClient"))
                                                                        .WithArgumentList(
                                                                            SyntaxFactory.ArgumentList()),
                                                                    SyntaxFactory.IdentifierName(
                                                                        "GetStringAsync")))
                                                            .WithArgumentList(
                                                                SyntaxFactory.ArgumentList(
                                                                    SyntaxFactory
                                                                        .SingletonSeparatedList<ArgumentSyntax>(
                                                                            SyntaxFactory.Argument(SyntaxFactory
                                                                                .InterpolatedStringExpression(
                                                                                    SyntaxFactory.Token(SyntaxKind
                                                                                        .InterpolatedStringStartToken))
                                                                                .WithContents(
                                                                                    SyntaxFactory
                                                                                        .List<
                                                                                            InterpolatedStringContentSyntax>(
                                                                                            new
                                                                                                InterpolatedStringContentSyntax
                                                                                                []
                                                                                                {
                                                                                                    SyntaxFactory
                                                                                                        .InterpolatedStringText()
                                                                                                        .WithTextToken(
                                                                                                            SyntaxFactory
                                                                                                                .Token(
                                                                                                                    SyntaxFactory
                                                                                                                        .TriviaList(),
                                                                                                                    SyntaxKind
                                                                                                                        .InterpolatedStringTextToken,
                                                                                                                    "http://localhost:8080" +
                                                                                                                    method
                                                                                                                        .GetUrl(),
                                                                                                                    "http://localhost:8080" +
                                                                                                                    method
                                                                                                                        .GetUrl(),
                                                                                                                    SyntaxFactory
                                                                                                                        .TriviaList()))
                                                                                                })))))))))))))));
                }

                if (method.GetHttpMethodName() == "Post")
                {
                    resultMethod = SyntaxFactory.MethodDeclaration(
                            SyntaxFactory.IdentifierName("Task<" + returnType + ">"),
                            SyntaxFactory.Identifier(method.GetMethodName()))
                        .WithParameterList(
                            SyntaxFactory.ParameterList(
                                SyntaxFactory.SeparatedList<ParameterSyntax>(arguments)))
                        .WithModifiers(
                            SyntaxFactory.TokenList(
                                new[]
                                {
                                    SyntaxFactory.Token(SyntaxKind.PublicKeyword),
                                    SyntaxFactory.Token(SyntaxKind.AsyncKeyword)
                                }))
                        .WithBody(
                            SyntaxFactory.Block(SyntaxFactory.SingletonList<StatementSyntax>(
                                SyntaxFactory.ReturnStatement(
                                    SyntaxFactory.AwaitExpression(
                                        SyntaxFactory.InvocationExpression(
                                                SyntaxFactory.MemberAccessExpression(
                                                    SyntaxKind.SimpleMemberAccessExpression,
                                                    SyntaxFactory.ObjectCreationExpression(
                                                            SyntaxFactory.IdentifierName("HttpClient"))
                                                        .WithArgumentList(
                                                            SyntaxFactory.ArgumentList()),
                                                    SyntaxFactory.IdentifierName("PostAsync")))
                                            .WithArgumentList(
                                                SyntaxFactory.ArgumentList(
                                                    SyntaxFactory.SeparatedList<ArgumentSyntax>(
                                                        new SyntaxNodeOrToken[]
                                                        {
                                                            SyntaxFactory.Argument(
                                                                SyntaxFactory.InterpolatedStringExpression(
                                                                        SyntaxFactory.Token(SyntaxKind
                                                                            .InterpolatedStringStartToken))
                                                                    .WithContents(
                                                                        SyntaxFactory
                                                                            .List<InterpolatedStringContentSyntax>(
                                                                                new InterpolatedStringContentSyntax[]
                                                                                {
                                                                                    SyntaxFactory
                                                                                        .InterpolatedStringText()
                                                                                        .WithTextToken(
                                                                                            SyntaxFactory.Token(
                                                                                                SyntaxFactory
                                                                                                    .TriviaList(), SyntaxKind.InterpolatedStringTextToken, "http://localhost:8080" +
                                                                                                method.GetUrl(), "http://localhost:8080" + method.GetUrl(), SyntaxFactory.TriviaList()))
                                                                                }))),
                                                            SyntaxFactory.Token(SyntaxKind.CommaToken),
                                                            SyntaxFactory.Argument(
                                                                SyntaxFactory.ObjectCreationExpression(
                                                                        SyntaxFactory.IdentifierName("StringContent"))
                                                                    .WithArgumentList(
                                                                        SyntaxFactory.ArgumentList(
                                                                            SyntaxFactory.SeparatedList<ArgumentSyntax>(
                                                                                new SyntaxNodeOrToken[]
                                                                                {
                                                                                    SyntaxFactory.Argument(
                                                                                        SyntaxFactory
                                                                                            .InvocationExpression(
                                                                                                SyntaxFactory
                                                                                                    .MemberAccessExpression(
                                                                                                        SyntaxKind
                                                                                                            .SimpleMemberAccessExpression,
                                                                                                        SyntaxFactory
                                                                                                            .IdentifierName(
                                                                                                                "JsonConvert"),
                                                                                                        SyntaxFactory
                                                                                                            .IdentifierName(
                                                                                                                "SerializeObject")))
                                                                                            .WithArgumentList(
                                                                                                SyntaxFactory
                                                                                                    .ArgumentList(
                                                                                                        SyntaxFactory
                                                                                                            .SingletonSeparatedList
                                                                                                                <ArgumentSyntax>(
                                                                                                                    SyntaxFactory
                                                                                                                        .Argument(
                                                                                                                            SyntaxFactory
                                                                                                                                .IdentifierName(
                                                                                                                                    method
                                                                                                                                        .GetNameArgWithRequestBody())))))),
                                                                                    SyntaxFactory.Token(SyntaxKind
                                                                                        .CommaToken),
                                                                                    SyntaxFactory.Argument(
                                                                                        SyntaxFactory
                                                                                            .MemberAccessExpression(
                                                                                                SyntaxKind
                                                                                                    .SimpleMemberAccessExpression,
                                                                                                SyntaxFactory
                                                                                                    .IdentifierName(
                                                                                                        "Encoding"),
                                                                                                SyntaxFactory
                                                                                                    .IdentifierName(
                                                                                                        "UTF8"))),
                                                                                    SyntaxFactory.Token(SyntaxKind
                                                                                        .CommaToken),
                                                                                    SyntaxFactory.Argument(
                                                                                        SyntaxFactory.LiteralExpression(
                                                                                            SyntaxKind
                                                                                                .StringLiteralExpression,
                                                                                            SyntaxFactory.Literal(
                                                                                                "application/json")))
                                                                                }))))
                                                        }))))))));
                   
                } 
                resultMethods.Add(resultMethod);
            }


            var compilationUnit = SyntaxFactory.CompilationUnit()
                .WithUsings(usingList)
                .AddMembers(namespaceDeclaration
                    .AddMembers(webClass
                        .WithMembers(
                            SyntaxFactory.List<MemberDeclarationSyntax>(
                                resultMethods))
                    )).NormalizeWhitespace();

            //Console.WriteLine(compilationUnit.ToFullString());

            string pathToSave = @"C:\Users\Иван\RiderProjects\Dolphin-in-river\lab-2\lab2\Client.cs";
            using (StreamWriter writer = new StreamWriter(pathToSave, false))
            {
                await writer.WriteLineAsync(compilationUnit.ToFullString());
            }
        }
    }
}