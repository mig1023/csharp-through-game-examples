using System;

namespace csharp_through_code_examples
{
    class Program
    {
        private static Random rand = new Random();

        private class Сharacter
        {
            public int Hitpoints { get; set; }

            public string Name { get; set; }

            public void Wound() =>
                Hitpoints -= rand.Next(5) + 1;
        }

        private class Hero : Сharacter
        {
            public string BeautifulSpeeches() =>
                "Oh, it hurts, but I won't give up!";
        }

        private class Goblin : Сharacter
        {
            public string Swearing() =>
                "Oh, *$?@#&!! I hate this %#@*!!";
        }

        static void Main()
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

            Console.WriteLine("You have {0} hitpoints and " +
                "gotta fight the {1} ({2} hitpoints)!\n" +
                "To hit him you have to type 'attack'!\n", 
                hero.Hitpoints, goblin.Name, goblin.Hitpoints);

            while ((goblin.Hitpoints > 0) && (hero.Hitpoints > 0))
            {
                Console.Write("> ");

                string action = Console.ReadLine();

                if (action.ToLower() == "attack")
                {
                    goblin.Wound();

                    Console.WriteLine("You hit him! " +
                        "His hitponts dropped to {0}", goblin.Hitpoints);

                    if (goblin.Hitpoints <= 0)
                        continue;

                    Console.WriteLine("{0}: {1}\n", goblin.Name, goblin.Swearing());

                    hero.Wound();

                    Console.WriteLine("{0} hit you! Your hitponts " +
                        "dropped to {1}", goblin.Name, hero.Hitpoints);

                    if (hero.Hitpoints > 0)
                        Console.WriteLine("{0}: {1}\n", hero.Name, hero.BeautifulSpeeches());
                }
                else
                {
                    Console.WriteLine("I did not understand you...\n");
                }
            }

            if (goblin.Hitpoints <= 0)
            {
                Console.WriteLine("\nYOU WON THIS BATTLE!");
            }
            else
            {
                Console.WriteLine("\nYou lost this fight...");
            }

            Console.ReadLine();
        }
    }
}
