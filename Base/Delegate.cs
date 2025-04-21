using System;
using System.Linq;

namespace csharp_through_code_examples
{
    class Program
    {
        private static Random random = new Random();

        delegate string EnemyScreams(int screamCount);

        static string EnemyHit(int hits, string enemyName, EnemyScreams scream) =>
            String.Format("{0} yells: {1}", enemyName, scream(hits));

        static string OrcScream(int screamCount) =>
            string.Concat(Enumerable.Repeat("Ouch! ", screamCount));

        static string GoblinScream(int screamCount) =>
            string.Concat(Enumerable.Repeat("Oops! ", screamCount));

        static void Main()
        {
            Console.WriteLine("Try to hit the enemy!\n");

            while (true)
            {
                EnemyScreams scream = null;

                Console.Write("How many hits > ");

                var success = int.TryParse(Console.ReadLine(), out int hits);
                var enemyName = String.Empty;

                if (!success || (hits <= 0))
                {
                    break;
                }
                else if (random.Next(100) % 2 == 0)
                {
                    enemyName = "Orc";
                    scream = OrcScream;
                }
                else
                {
                    enemyName = "Goblin";
                    scream = GoblinScream;
                }

                Console.WriteLine(EnemyHit(hits, enemyName, scream));
            }
        }
    }
}
