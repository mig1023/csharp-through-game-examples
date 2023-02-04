using System;
using System.Collections.Generic;

namespace csharp_through_code_examples
{
    interface Subject
    {
        void Register(Observer observer);
        void Remove(Observer observer);
        void Hit(int hitpoints);
    }

    interface Observer
    {
        void Update(int hitpoints);
    }

    class Hero : Subject
    {
        List<Observer> enemyList = new List<Observer>();

        public void Register(Observer enemy) => enemyList.Add(enemy);
        public void Remove(Observer enemy) => enemyList.Remove(enemy);
        public void Hit(int hitpoints)
        {
            foreach (Observer enemy in enemyList)
                enemy.Update(hitpoints);
        }
    }

    class Orc : Observer
    {
        int Hitpoints { get; set; }

        public Orc(int hitpoints) => Hitpoints = hitpoints;

        public void Update(int hitpoints)
        {
            Hitpoints -= hitpoints;

            if (Hitpoints > 0)
                Console.WriteLine("Ah! Orc wounded! Now hitpoints: {0}", Hitpoints);
        }
    }

    class Goblin : Observer
    {
        int Hitpoints { get; set; }

        public Goblin(int hitpoints) => Hitpoints = hitpoints;

        public void Update(int hitpoints)
        {
            Hitpoints -= hitpoints;

            if (Hitpoints > 0)
                Console.WriteLine("Oh! Goblin wounded! Now hitpoints: {0}", Hitpoints);
        }
    }

    class Program
    {
        static void Main()
        {
            Hero hero = new Hero();

            Orc orc = new Orc(hitpoints: 30);
            Goblin goblin = new Goblin(hitpoints: 20);

            hero.Register(enemy: orc);
            hero.Register(enemy: goblin);

            string action = String.Empty;

            Console.WriteLine("This is another epic battle!\n");

            do
            {
                Console.Write("Strength of hero hit > ");
                action = Console.ReadLine();

                if (int.TryParse(action, out int strength))
                    hero.Hit(hitpoints: strength);
            }
            while (!String.IsNullOrEmpty(action));
        }
    }
}
