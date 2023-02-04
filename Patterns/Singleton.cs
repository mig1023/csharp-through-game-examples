using System;

namespace csharp_through_code_examples
{
    class Program
    {

        public class Counter
        {
            private static Counter instance = new Counter();

            private Counter() { }

            public static Counter NewInstance() => instance;

            private int counter = 99;

            public int Get() => --instance.counter;
        }

        static void Main()
        {
            int bottles = 99;

            do
            {
                Console.WriteLine("{0} bottles of beer on " +
                    "the wall", bottles);
                Console.WriteLine("{0} bottles of beer!", bottles);
                Console.WriteLine("Take one down, pass it around", bottles);

                Counter bottleCounter = Counter.NewInstance();
                bottles = bottleCounter.Get();

                Console.WriteLine("{0} bottles of beer on the wall!", bottles);
                Console.ReadLine();
            }
            while (bottles > 1);

            Console.WriteLine("1 bottle of beer on the wall\n1 bottle of beer!");
            Console.WriteLine("Take one down, pass it around\nNo bottles of beer on the wall");
        }
    }
}
