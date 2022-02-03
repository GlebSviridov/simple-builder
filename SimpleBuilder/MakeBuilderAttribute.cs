using System;

namespace SimpleBuilder
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class MakeBuilderAttribute : Attribute
    {
        public MakeBuilderAttribute(Type classUnderBuilder)
        {
            ClassUnderBuilder = classUnderBuilder;
        }

        public Type ClassUnderBuilder { get; }
    }
}