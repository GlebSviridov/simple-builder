using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace SimpleBuilder
{
    public readonly struct BuilderToGenerate
    {
        public readonly INamedTypeSymbol ClassUnderBuilder;
        public readonly List<IPropertySymbol> Properties;
        public readonly string BuilderName;

        public BuilderToGenerate(INamedTypeSymbol classUnderBuilder, List<IPropertySymbol> properties)
        {
            ClassUnderBuilder = classUnderBuilder;
            Properties = properties;
            BuilderName = classUnderBuilder.Name + "Builder";
        }
    }
}