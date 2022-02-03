using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SimpleBuilder
{
    public class SyntaxReceiver : ISyntaxContextReceiver
    {
        public List<string> Log { get; } = new();

        public List<WorkItem> WorkItems { get; } = new();

        public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
        {
            try
            {
                if (context.Node is ClassDeclarationSyntax classDeclarationSyntax)
                {
                    var classForBuilder = (INamedTypeSymbol)context.SemanticModel.GetDeclaredSymbol(context.Node)!;
                    Log.Add($"Found a class named {classForBuilder.Name}");

                    var attributes = classForBuilder.GetAttributes();
                    Log.Add($"    Found {attributes.Length} attributes");

                    foreach (AttributeData att in attributes)
                    {
                        Log.Add(
                            $"   Attribute: {att.AttributeClass!.Name} Full Name: {att.AttributeClass.FullNamespace()}");
                        foreach (var arg in att.ConstructorArguments)
                        {
                            Log.Add(
                                $"    ....Argument: Type='{arg.Type}' Value_Type='{arg.Value?.GetType().FullName}' Value='{arg.Value}'");
                        }
                    }

                    var makeBuilderAttribute = classForBuilder
                        .GetAttributes()
                        .FirstOrDefault(att => att.AttributeClass.FullName() == "SourceBuilder.MakeBuilderAttribute");

                    if (makeBuilderAttribute is not null)
                    {
                        var classUnderAttribute = (INamedTypeSymbol?)makeBuilderAttribute.ConstructorArguments[0].Value;


                        if (classUnderAttribute != null)
                        {
                            WorkItems.Add(new(classForBuilder.FullName() + "Builder", classUnderAttribute));
                            Log.Add($"Added work item for {classUnderAttribute.FullName()} - {classForBuilder}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Add("Error parsing syntax: " + ex);
            }
        }
    }
}