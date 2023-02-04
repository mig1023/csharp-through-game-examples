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

        static void Main()
        {
            Goblin goblin = new Goblin
            {
                Hitpoints = rand.Next(20) + 10,
            };

            Console.WriteLine("You have to fight the {0} ({1} hitpoints)!\n" +
                "To hit him you have to type 'hit'!\n", goblin.Name(), goblin.Hitpoints);

            while (goblin.Hitpoints > 0)
            {
                Console.Write("> ");

                string action = Console.ReadLine();

                if (action.ToLower() == "hit")
                {
                    goblin.Wound();

                    Console.WriteLine("You hit him! His hitponts dropped to {0}\n",
                        goblin.Hitpoints);
                }
                else
                    Console.WriteLine("I did not understand you...\n");
            }

            Console.WriteLine("YOU WON THIS BATTLE!\nNot surprisingly, but still...");
            Console.ReadLine();
        }
    }
}
