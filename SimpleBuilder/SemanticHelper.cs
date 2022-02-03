using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace SimpleBuilder
{
    public static class SemanticHelper
    {
        public static string? FullName(this INamedTypeSymbol? symbol)
        {
            if (symbol == null)
                return null;

            var prefix = FullNamespace(symbol);
            var suffix = "";
            if (symbol.Arity > 0)
            {
                suffix = "<"
                    + string.Join(", ", symbol.TypeArguments.Select(t => FullName((INamedTypeSymbol)t)))
                    + ">";
            }

            if (prefix != "")
                return prefix + "." + symbol.Name + suffix;
            else
                return symbol.Name + suffix;
        }

        public static string FullNamespace(this ISymbol symbol)
        {
            var parts = new Stack<string>();
            INamespaceSymbol? iterator = (symbol as INamespaceSymbol) ?? symbol.ContainingNamespace;
            while (iterator != null)
            {
                if (!string.IsNullOrEmpty(iterator.Name))
                    parts.Push(iterator.Name);
                iterator = iterator.ContainingNamespace;
            }

            return string.Join(".", parts);
        }


        public static bool HasDefaultConstructor(this INamedTypeSymbol symbol)
        {
            return symbol.Constructors.Any(c => c.Parameters.Count() == 0);
        }

        public static IEnumerable<IPropertySymbol> ReadWriteScalarProperties(this INamedTypeSymbol symbol)
        {
            return symbol.GetMembers().OfType<IPropertySymbol>()
                .Where(p =>  p.CanRead() && p.CanWrite() && !p.HasParameters());
        }

        private static bool CanRead(this IPropertySymbol symbol) => symbol.GetMethod != null;

        private static bool CanWrite(this IPropertySymbol symbol) => symbol.SetMethod != null;

        private static bool HasParameters(this IPropertySymbol symbol) => symbol.Parameters.Any();
    }
}