using System;

namespace csharp_through_code_examples
{
    class Program
    {
        private class Gandalf : IDisposable
        {
            public Gandalf() => Console.WriteLine("Gandalf was born in 2500 of the Third Age!");

            public void Dispose() => Console.WriteLine("Hey, Gandalf, you have to go!");

            ~Gandalf() => Console.WriteLine("Gandalf sailed away at 3200 of the Third Age!");
        }

        static void GandalfHistory()
        {
            Gandalf gandalf = new Gandalf();

            while (true)
            {
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    gandalf.Dispose();
                    break;
                }
                else
                    Console.WriteLine("Gandalf does something good!");
            }
        }

        static void Main()
        {
            GandalfHistory();

            Console.WriteLine("This is end of the Gandalf story!");

            GC.Collect();

            Console.ReadLine();
        }
    }
}
