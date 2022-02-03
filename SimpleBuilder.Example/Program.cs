using System;

namespace SimpleBuilder.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var builded = TestClassBuilder.CreateDefault().WithTestIntProperty(123).WithTestString("skjf")
                .WithNullableInt(null).Build();
            Console.WriteLine($"{builded.TestIntProperty} - {builded.TestString} - {builded.NullableInt}");
        }
    }

    [MakeBuilder(typeof(TestClass))]
    class TestClass
    {
        public string TestString { get; set; }

        public int TestIntProperty { get; set; }

        public int? NullableInt { get; set; }
    }
}