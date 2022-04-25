using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Lab2
{
    public class DTOGenerator
    {
        public async Task Do(ParserDTO parserDto)
        {
            foreach (var item in parserDto.declarationsDTO)
            {
                //-------keywords-------
                SyntaxTokenList publicKey = SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword));
                SyntaxTokenList privateKey = SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PrivateKeyword));
                //-------usings-------
                var system1 = SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName("System.Collections.Generic"));
                var usingList = SyntaxFactory.List(new[] {system1});

                //-------namespace-------
                var namespaceDeclaration = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.IdentifierName("Lab2"));

                //-------classname-------

                var webClass = SyntaxFactory.ClassDeclaration(item.ClassName).WithModifiers(publicKey);

                //-------fields, getters and setters + constructor-------

                var resultFields = new List<FieldDeclarationSyntax>();
                var resultGetters = new List<MethodDeclarationSyntax>();
                foreach (var field in item.Fields)
                {
                    var fields =
                        SyntaxFactory.FieldDeclaration(
                                SyntaxFactory.VariableDeclaration(
                                        SyntaxFactory.IdentifierName(field.GetReturnType()))
                                    .WithVariables(
                                        SyntaxFactory.SingletonSeparatedList<VariableDeclaratorSyntax>(
                                            SyntaxFactory.VariableDeclarator(
                                                SyntaxFactory.Identifier(field.GetName())))))
                            .WithModifiers(publicKey);
                    resultFields.Add(fields);
                }


                int length = Math.Max(item.Args.Count * 2 - 1, 0);
                var arguments = new SyntaxNodeOrToken[length];
                int counter = 0;
                for (int i = 0; i < length; i++)
                {
                    if (i % 2 == 0)
                    {
                        var currentlyArgument = SyntaxFactory
                            .Parameter(SyntaxFactory.Identifier(item.Args[counter].GetName()))
                            .WithType(
                                SyntaxFactory.IdentifierName(item.Args[counter].GetType()));

                        arguments[i] = currentlyArgument;
                        counter++;
                    }

                    if (i % 2 == 1)
                    {
                        arguments[i] = SyntaxFactory.Token(SyntaxKind.CommaToken);
                    }
                }

                var body = new List<StatementSyntax>();
                for (int j = 0; j < item.Args.Count; j++)
                {
                    body.Add(SyntaxFactory.ExpressionStatement(
                        SyntaxFactory.AssignmentExpression(
                            SyntaxKind.SimpleAssignmentExpression,
                            SyntaxFactory.IdentifierName(item.Fields[j].GetName()),
                            SyntaxFactory.IdentifierName(item.Args[j].GetName()))));
                }

                var constructor = SyntaxFactory.ConstructorDeclaration(
                        SyntaxFactory.Identifier(item.ClassName))
                    .WithModifiers(
                        SyntaxFactory.TokenList(
                            SyntaxFactory.Token(SyntaxKind.PublicKeyword)))
                    .WithParameterList(
                        SyntaxFactory.ParameterList(
                            SyntaxFactory.SeparatedList<ParameterSyntax>(arguments)))
                    .WithBody(
                        SyntaxFactory.Block(body));


                foreach (var field in item.Fields)
                {
                    var getter = SyntaxFactory.MethodDeclaration(
                            SyntaxFactory.IdentifierName(field.GetReturnType()),
                            SyntaxFactory.Identifier("Get" + field.GetName()))
                        .WithModifiers(publicKey)
                        .WithBody(
                            SyntaxFactory.Block(
                                SyntaxFactory.SingletonList<StatementSyntax>(
                                    SyntaxFactory.ReturnStatement(
                                        SyntaxFactory.IdentifierName(field.GetName())))));
                    var setter = SyntaxFactory.MethodDeclaration(
                            SyntaxFactory.PredefinedType(
                                SyntaxFactory.Token(SyntaxKind.VoidKeyword)),
                            SyntaxFactory.Identifier("Set" + field.GetName()))
                        .WithModifiers(publicKey)
                        .WithParameterList(
                            SyntaxFactory.ParameterList(
                                SyntaxFactory.SingletonSeparatedList<ParameterSyntax>(
                                    SyntaxFactory.Parameter(
                                            SyntaxFactory.Identifier("newValue"))
                                        .WithType(
                                            SyntaxFactory.IdentifierName(field.GetReturnType())))))
                        .WithBody(
                            SyntaxFactory.Block(
                                SyntaxFactory.SingletonList<StatementSyntax>(
                                    SyntaxFactory.ExpressionStatement(
                                        SyntaxFactory.AssignmentExpression(
                                            SyntaxKind.SimpleAssignmentExpression,
                                            SyntaxFactory.IdentifierName(field.GetName()),
                                            SyntaxFactory.IdentifierName("newValue"))))));

                    resultGetters.Add(getter);
                    resultGetters.Add(setter);
                }


                var compilationUnit = SyntaxFactory.CompilationUnit()
                    .WithUsings(usingList)
                    .AddMembers(namespaceDeclaration
                        .AddMembers(webClass
                            .AddMembers(resultFields.ToArray())
                            .AddMembers(constructor)
                            .AddMembers(resultGetters.ToArray())
                        )).NormalizeWhitespace();
                string pathToSave = @"C:\Users\Иван\RiderProjects\Dolphin-in-river\lab-2\lab2\" + item.ClassName + ".cs";
                using (StreamWriter writer = new StreamWriter(pathToSave, false))
                {
                    await writer.WriteLineAsync(compilationUnit.ToFullString());
                }
                //Console.WriteLine(compilationUnit.ToFullString());
            }
        }
    }
}