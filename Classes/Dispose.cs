using System;

namespace csharp_through_code_examples
{
    class Program
    {
        private class Gandalf : IDisposable
        {
            public Gandalf() => Console.WriteLine("[constructor]\t\tGandalf was born in 2500 of the Third Age!");

            public void Dispose() => Console.WriteLine("[dispose call]\t\tHey, Gandalf, you have to go!");

            public void OrdinaryMagic() => Console.WriteLine("\t\t\tGandalf does something good!");

            ~Gandalf() => Console.WriteLine("[finalizer]\t\tGandalf sailed away at 3200 of the Third Age!");
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
                {
                    gandalf.OrdinaryMagic();
                }
            }
        }

        static void GarbageCleaning()
        {
            Console.WriteLine("[garbage collector]\tThis is end of the Gandalf story!");
            GC.Collect();
        }

        static void Main()
        {
            GandalfHistory();

            GarbageCleaning();

            Console.ReadKey();
        }
    }
}
