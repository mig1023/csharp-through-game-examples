using System;

class Program
{
    interface IAttack
    {
        void Execute();
    }

    class Hero
    {
        string heroName = String.Empty;

        public Hero(string name) => heroName = name;

        public void Attack() => Console.WriteLine("\n{0} attack!!", heroName);
    }

    class AttackCommand : IAttack
    {
        Hero hero;

        public AttackCommand(Hero heroSet) => hero = heroSet;

        public void Execute() => hero.Attack();
    }

    class Fight
    {
        IAttack attack;

        public void SetAttack(IAttack setAttack) => attack = setAttack;

        public void Attack() => attack.Execute();
    }

    static void Main()
    {
        Console.WriteLine("Welcome to Duelling Club of Hogwarts!\n" +
            "Press 'H' for Harry Potter attack, press 'D' for Draco Malfoy attack...\n");

        Fight fight = new Fight();

        Hero harryPotter = new Hero("Harry Potter");
        Hero dracoMalfoy = new Hero("Draco Malfoy");
        
        do
        {
            ConsoleKeyInfo action = Console.ReadKey();

            if (action.Key == ConsoleKey.H)
                fight.SetAttack(new AttackCommand(harryPotter));

            else if (action.Key == ConsoleKey.D)
                fight.SetAttack(new AttackCommand(dracoMalfoy));

            else if (action.Key == ConsoleKey.Enter)
                break;

            else
            {
                Console.WriteLine("\nI don't know who this is: '{0}'!", action.Key.ToString());
                continue;
            }

            fight.Attack();
        }
        while (true);
    }
}
