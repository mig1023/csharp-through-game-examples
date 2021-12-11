# С# through code examples

## Content

0. [Hello World](#hello-world)
1. [Class Encapsulation](#class-encapsulation)
2. [Class Inheritance](#class-inheritance)
3. [Class Dispose](#class-dispose)

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

## Class Inheritance

```c#
sing System;

namespace csharp_through_code_examples
{
    class Program
    {
        private static Random rand = new Random();

        private class Сharacter
        {
            public int Hitpoints { get; set; }

            public string Name { get; set; }

            public void Wound() => Hitpoints -= rand.Next(5) + 1;
        }

        private class Hero : Сharacter
        {
            public string BeautifulSpeeches() => "Oh, it hurts, but I won't give up!";
        }

        private class Goblin : Сharacter
        {
            public string Swearing() => "Oh, *$?@#&!! I hate this %#@*!!";
        }

        static void Main(string[] args)
        {
            Hero hero = new Hero
            {
                Name = "You",
                Hitpoints = rand.Next(20) + 10,
            };

            Goblin goblin = new Goblin
            {
                Name = "Ugly goblin",
                Hitpoints = rand.Next(20) + 10,
            };

            Console.WriteLine(String.Format("You have {0} hitpoints and gotta fight the {1} ({2} hitpoints)!\n" +
                "To hit him you have to type 'attack'!\n", hero.Hitpoints, goblin.Name, goblin.Hitpoints));

            while ((goblin.Hitpoints > 0) && (hero.Hitpoints > 0))
            {
                Console.Write("> ");

                string action = Console.ReadLine();

                if (action.ToLower() == "attack")
                {
                    goblin.Wound();

                    Console.WriteLine(String.Format("You hit him! His hitponts dropped to {0}",
                        goblin.Hitpoints));

                    if (goblin.Hitpoints > 0)
                        Console.WriteLine(String.Format("{0}: {1}\n", goblin.Name, goblin.Swearing()));

                    hero.Wound();

                    Console.WriteLine(String.Format("{0} hit you! Your hitponts dropped to {1}",
                        goblin.Name, hero.Hitpoints));

                    if (hero.Hitpoints > 0)
                        Console.WriteLine(String.Format("{0}: {1}\n", hero.Name, hero.BeautifulSpeeches()));
                }
                else
                    Console.WriteLine("I did not understand you...\n");
            }

            if (goblin.Hitpoints < 0)
                Console.WriteLine("YOU WON THIS BATTLE!");
            else
                Console.WriteLine("You lost this fight...");

            Console.ReadLine();
        }
    }
}
```

## Class Dispose

```c#
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

        static void Main(string[] args)
        {
            GandalfHistory();

            Console.WriteLine("This is end of the Gandalf story!");

            GC.Collect();

            Console.ReadLine();
        }
    }
}
```
