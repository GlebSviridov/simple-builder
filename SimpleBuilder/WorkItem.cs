using System;
using Microsoft.CodeAnalysis;

namespace SimpleBuilder
{
    public class WorkItem
    {
        public WorkItem(
            string builderClass,
            INamedTypeSymbol classUnderBuilder)
        {
            BuilderClass = builderClass ?? throw new ArgumentNullException(nameof(builderClass));
            ClassUnderBuilder = classUnderBuilder ?? throw new ArgumentNullException(nameof(classUnderBuilder));
        }

        public INamedTypeSymbol ClassUnderBuilder { get; }

        public string BuilderClass { get; }
    }
}