using System;

class Program
{
    class Hero
    {
        public int Punch()
        {
            Console.WriteLine("Punch!");
            return 5;
        }

        public int Kick()
        {
            Console.WriteLine("Kick!");
            return 6;
        }

        public int Headbutt()
        {
            Console.WriteLine("Headbutt!");
            return 3;
        }

        public int ElbowStrike()
        {
            Console.WriteLine("Elbow strike!");
            return 4;
        }

        public int KneeStrike()
        {
            Console.WriteLine("Knee strike!");
            return 5;
        }
    }

    class Combo
    {
        Hero hero;

        public Combo(Hero setHero) => hero = setHero;

        public int Make()
        {
            Console.WriteLine("Combo...");

            int damage = 0;

            Console.Write("...");
            damage += hero.Kick();

            Console.Write("...");
            damage += hero.Punch();

            Console.Write("...");
            damage += hero.ElbowStrike();

            Console.Write("...");
            damage += hero.KneeStrike();

            return damage;
        }
    }

    class EvilOrc
    {
        private int _hitponts;
        public int Hitpoints
        {
            get => _hitponts;
            
            set
            {
                _hitponts = value;
                Console.WriteLine("\tEvil Orc said: AUCH!! {0} hitpoints left.", _hitponts);
            }
        }

        public EvilOrc(int setHitpoints) => _hitponts = setHitpoints;
    }

    static void Main()
    {
        Console.WriteLine("Fight against Evel Orc!");
        Console.WriteLine("'P' - punch, 'K' - kick, 'H' - headbutt");
        Console.WriteLine("'E' - elbow strike, 'N' - knee strike, 'C' - combo");

        EvilOrc orc = new EvilOrc(50);
        Hero hero = new Hero();

        Combo combo = new Combo(hero);

        do
        {
            ConsoleKeyInfo action = Console.ReadKey();
            Console.WriteLine();

            if (action.Key == ConsoleKey.P)
                orc.Hitpoints -= hero.Punch();

            else if (action.Key == ConsoleKey.K)
                orc.Hitpoints -= hero.Kick();

            else if (action.Key == ConsoleKey.H)
                orc.Hitpoints -= hero.Headbutt();

            else if (action.Key == ConsoleKey.E)
                orc.Hitpoints -= hero.ElbowStrike();

            else if (action.Key == ConsoleKey.N)
                orc.Hitpoints -=  hero.KneeStrike();

            else if (action.Key == ConsoleKey.C)
                orc.Hitpoints -= combo.Make();

            else
                Console.WriteLine("I did not understand you...\n");
        }
        while (orc.Hitpoints > 0);

        Console.WriteLine("\nYOU WON THIS BATTLE!\nNot surprisingly, but still...");
        Console.ReadLine();
    }
}
