# С# through code examples

## Content

0. [Hello World](#hello-world)
1. [Class Encapsulation](#class-encapsulation)

## Hello World

```c#
System;

namespace hello_world
{
    class Program
    {
        private static Random rand = new Random();

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World! It's guessing game!\n");

            int secret = rand.Next(100) + 1;
            int attempt = 0;
            int attemptsNumber = 0;

            do
            {
                attemptsNumber += 1;

                Console.Write(String.Format("Your {0}try: ",
                    (attemptsNumber > 1 ? "next " : String.Empty)));

                bool thisIsNumber = int.TryParse(Console.ReadLine(), out attempt);

                if (!thisIsNumber)
                    Console.WriteLine("It doesn't look like a number...\n");

                else if (attempt > secret)
                    Console.WriteLine("Overshoot!\n");

                else if (attempt < secret)
                    Console.WriteLine("Undershoot!\n");

                else
                    Console.WriteLine(String.Format("Exactly! You WIN!\n\n" +
                        "Number of attempts: {0}",attemptsNumber));
            }
            while (secret != attempt);

            Console.ReadLine();
        }
    }
}
```
## Class Encapsulation 

```c#
using System;

namespace csharp_through_code_examples
{
    class Program
    {
        private static Random rand = new Random();

        private class Goblin
        {
            public int Hitpoints { get; set; }

            public string Name() => "ugly goblin";

            public void Wound()
            {
                Hitpoints -= rand.Next(5) + 1;
            }
        }

        static void Main(string[] args)
        {
            Goblin goblin = new Goblin
            {
                Hitpoints = rand.Next(20) + 10,
            };

            Console.WriteLine(String.Format("You have to fight the {0} ({1} hitpoints)!\n" +
                "To hit him you have to type 'hit'!\n", goblin.Name(), goblin.Hitpoints));

            while (goblin.Hitpoints > 0)
            {
                Console.Write("> ");

                string action = Console.ReadLine();

                if (action.ToLower() == "hit")
                {
                    goblin.Wound();

                    Console.WriteLine(String.Format("You hit him! His hitponts dropped to {0}\n",
                        goblin.Hitpoints));
                }
                else
                    Console.WriteLine("I did not understand you...\n");
            }

            Console.WriteLine("YOU WON THIS BATTLE! Not surprisingly, but still...");
            Console.ReadLine();
        }
    }
}
```
