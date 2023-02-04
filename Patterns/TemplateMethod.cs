using System;
using System.Collections.Generic;

class Program
{
    abstract class Spell
    {
        public abstract string SpellName();

        public string Speak() => String.Format("You say: {0}...", SpellName());

        public string WandGesture() => "You wave your wand...";

        public abstract string MagicEffect();
    }

    class Accio : Spell
    {
        public override string SpellName() => "Accio";
        public override string MagicEffect() => "Objects are flying towards us!";
    }

    class Reducto : Spell
    {
        public override string SpellName() => "Reducto";
        public override string MagicEffect() => "Objects are broken and destroyed!";
    }

    class Lumos : Spell
    {
        public override string SpellName() => "Lumos";
        public override string MagicEffect() => "The bright light is on!";
    }
    
    class Riddikulus : Spell
    {
        public override string SpellName() => "Riddikulus";
        public override string MagicEffect() => "Boggart was ridiculed!";
    }

    static void Main()
    {
        Console.WriteLine("Hi!\nMy name is Professor Lupin!\nNow we will learn a few spells!");

        List<Spell> spells = new List<Spell>
        {
            new Accio(),
            new Reducto(),
            new Lumos(),
            new Riddikulus(),
        };

        foreach (Spell spell in spells)
        {
            Console.ReadLine();

            Console.WriteLine(spell.Speak());
            Console.WriteLine(spell.WandGesture());
            Console.WriteLine(spell.MagicEffect());
        }

        Console.WriteLine("\nIt's all for today!\nLesson is over!");
    }
}
