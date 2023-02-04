using System;
using System.Collections.Generic;

class Program
{
    class Hero
    {
        public string Name;
        public int Age;
        public string Weapon;
    }

    class Orc : Hero
    {
        public int VillainyLevel;

        public static IEnumerable<Orc> CreateIterator(List<Orc> orcs)
        {
            foreach (Orc orc in orcs)
                yield return orc;
        }
    }

    class Elf : Hero
    {
        public int NobilityLevel;

        public static IEnumerable<Elf> CreateIterator(Dictionary<string, Elf> elfs)
        {
            foreach (Elf elf in elfs.Values)
                yield return elf;
        }
    }

    static void PrintAll(IEnumerable<Hero> heroes)
    {
        int i = 0;

        foreach (Hero hero in heroes)
            Console.WriteLine("\t{0}. {1} with {2}.", ++i, hero.Name, hero.Weapon);
    }
    
    static void Main()
    {
        List<Orc> orcs = new List<Orc>();

        orcs.Add(new Orc { Name = "Azog", Age = 5, Weapon = "sword", VillainyLevel = 10});
        orcs.Add(new Orc { Name = "Ugluk", Age = 2, Weapon = "axe", VillainyLevel = 5 });
        orcs.Add(new Orc { Name = "Grishnakh", Age = 3, Weapon = "knife", VillainyLevel = 6 });

        Dictionary<string, Elf> elfs = new Dictionary<string, Elf>();

        elfs.Add("Legolas", new Elf { Name = "Legolas", Age = 1000, Weapon = "bow", NobilityLevel = 9 });
        elfs.Add("Galadriel", new Elf { Name = "Galadriel", Age = 5000, Weapon = "magic", NobilityLevel = 10 });
        elfs.Add("Haldir", new Elf { Name = "Haldir", Age = 1000, Weapon = "sword", NobilityLevel = 9 });

        Console.WriteLine("ORCS:");
        PrintAll(Orc.CreateIterator(orcs));

        Console.WriteLine("ELFS:");
        PrintAll(Elf.CreateIterator(elfs));

        Console.ReadLine();
    }
}
