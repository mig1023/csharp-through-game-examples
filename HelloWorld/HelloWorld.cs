using System;

namespace hello_world
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello World!\nIt's guessing game!\n");

            var secret = new Random().Next(100) + 1;
            var attempt = 0;
            var count = 0;

            do
            {
                count += 1;

                Console.Write("Your {0}try: ", count > 1 ? "next " : String.Empty);

                var thisIsNumber = int.TryParse(Console.ReadLine(), out attempt);

                if (!thisIsNumber)
                {
                    Console.WriteLine("It doesn't look like a number...\n");
                }
                else if (attempt > secret)
                {
                    Console.WriteLine("Overshoot!\n");
                }
                else if (attempt < secret)
                {
                    Console.WriteLine("Undershoot!\n");
                }
                else
                {
                    Console.WriteLine("Exactly! You WIN!\n\nNumber of attempts: {0}", count);
                }
            }
            while (secret != attempt);

            Console.ReadLine();
        }
    }
}
