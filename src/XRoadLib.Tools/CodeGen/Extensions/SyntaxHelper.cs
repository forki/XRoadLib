using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace XRoadLib.Tools.CodeGen.Extensions
{
    public static class SyntaxHelper
    {
        public static TypeSyntax TypeSyntax<T>()
        {
            return ParseTypeName(GetTypeName(typeof(T)));
        }

        private static string GetTypeName(Type type)
        {
            var name = type.Name.Replace("+", ".");
            if (name.Contains("`"))
                name = name.Substring(0, name.IndexOf("`"));
            return name;
        }
    }
}