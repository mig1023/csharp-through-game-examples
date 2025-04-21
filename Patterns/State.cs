using System;

class Program
{
    abstract class Healthbar
    {
        public abstract void Wound(EvilOrc orc);
    }

    class Healthy : Healthbar
    {
        public override void Wound(EvilOrc orc)
        {
            Console.WriteLine("Auch! Evil orc is injured now!");
            orc.Health = new Injured();
        }
    }

    class Injured : Healthbar
    {
        public override void Wound(EvilOrc orc)
        {
            Console.WriteLine("Auch! Evil orc is seriously wounded now!");
            orc.Health = new Wounded();
        }
    }

    class Wounded : Healthbar
    {
        public override void Wound(EvilOrc orc)
        {
            Console.WriteLine("Auch! Evil orc is killed!");
            orc.Health = new Killed();
        }
    }

    class Killed : Healthbar
    {
        public override void Wound(EvilOrc orc)
        {
            Console.WriteLine("No abuse of the corpse, please!");
        }
    }

    class EvilOrc
    {
        public Healthbar Health { get; set; }

        public EvilOrc(Healthbar startState) => 
            Health = startState;

        public void Punch() => 
            Health.Wound(this);
    }

    static void Main()
    {
        Console.WriteLine("Fight against Evil Orc!");
        Console.WriteLine("Ented 'punch' for attack...\n");

        var orc = new EvilOrc(new Healthy());

        do
        {
            Console.Write("> ");
            var action = Console.ReadLine();

            if (String.IsNullOrEmpty(action))
            {
                break;
            }
            else if (action == "punch")
            {
                orc.Punch();

                if (orc.Health is Killed)
                    Console.WriteLine("YOU WIN!\nNot surprisingly, but still...");
            }
            else
            {
                Console.WriteLine("I did not understand you...");
            }

        }
        while (true);
    }
}
