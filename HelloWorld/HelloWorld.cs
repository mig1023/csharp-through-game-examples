using System;

namespace hello_world
{
    class Program
    {
        private static Random rand = new Random();

        static void Main()
        {
            Console.WriteLine("Hello World!\nIt's guessing game!\n");

            int secret = rand.Next(100) + 1;
            int attempt = 0;
            int attemptsNumber = 0;

            do
            {
                attemptsNumber += 1;

                Console.Write("Your {0}try: ", attemptsNumber > 1 ? "next " : String.Empty);

                bool thisIsNumber = int.TryParse(Console.ReadLine(), out attempt);

                if (!thisIsNumber)
                    Console.WriteLine("It doesn't look like a number...\n");

                else if (attempt > secret)
                    Console.WriteLine("Overshoot!\n");

                else if (attempt < secret)
                    Console.WriteLine("Undershoot!\n");

                else
                    Console.WriteLine("Exactly! You WIN!\n\nNumber of attempts: {0}", attemptsNumber);
            }
            while (secret != attempt);

            Console.ReadLine();
        }
    }
}
