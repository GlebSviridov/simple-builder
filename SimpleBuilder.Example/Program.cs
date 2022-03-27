using System;

namespace SimpleBuilder.Example
{
    public static class Program
    {
        private static void Main()
        {
            var builded = TestClassBuilder.CreateDefault().WithTestIntProperty(123).WithTestString("skjf")
                .WithNullableInt(null).Build();
            Console.WriteLine($"{builded.TestIntProperty} - {builded.TestString} - {builded.NullableInt}");
            var person = PersonBuilder.CreateDefault().Build();
            Console.WriteLine($"{person.Name} - {person.LastName}");
        }
    }

    [MakeBuilder(typeof(TestClass))]
    internal class TestClass
    {
        public string TestString { get; set; }

        public int TestIntProperty { get; set; }

        public int? NullableInt { get; set; }
    }

    [MakeBuilder(typeof(Person))]
    internal class Person
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }
    }
}