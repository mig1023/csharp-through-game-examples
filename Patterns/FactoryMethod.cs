using System;
using System.Collections.Generic;

namespace csharp_through_code_examples
{
    class Program
    {
        public abstract class Hero
        {
            protected string Name;
            public abstract string Description();
        }

        public abstract class HeroCreator
        {
            public abstract Hero HeroFactoryMethod();
        }

        public class Human : Hero
        {
            public Human(string name) => Name = name;
            public override string Description() => String.Format("human {0}", Name);
        }

        public class HumanCreator : HeroCreator
        {
            private List<string> names = new List<string> { "Aragorn", "Boromir", "Faramir" };
            private static int nameIndex = -1;

            public override Hero HeroFactoryMethod()
            {
                nameIndex += 1;

                if (nameIndex >= names.Count)
                    return null;
                else
                    return new Human(names[nameIndex]);
            }
        }

        public class Elf : Hero
        {
            public Elf(string name) => Name = name;
            public override string Description() => String.Format("elf {0}", Name);
        }

        public class ElfCreator : HeroCreator
        {
            private List<string> names = new List<string> { "Legolas", "Galadriel", "Haldir" };
            private static int nameIndex = -1;

            public override Hero HeroFactoryMethod()
            {
                nameIndex += 1;

                if (nameIndex >= names.Count)
                    return null;
                else
                    return new Elf(names[nameIndex]);
            }
        }

        public class Orc : Hero
        {
            public Orc(string name) => Name = name;
            public override string Description() => String.Format("orc {0}", Name);
        }

        public class OrcCreator : HeroCreator
        {
            private List<string> names = new List<string> { "Azog", "Ugluk", "Grishnakh" };
            private static int nameIndex = -1;

            public override Hero HeroFactoryMethod()
            {
                nameIndex += 1;

                if (nameIndex >= names.Count)
                    return null;
                else
                    return new Orc(names[nameIndex]);
            }
        }

        public static HeroCreator Creator(string type)
        {
            switch (type.ToLower())
            {
                case "human":
                    return new HumanCreator();

                case "elf":
                    return new ElfCreator();

                case "orc":
                    return new OrcCreator();

                default:
                    return null;
            }
        }

        static void Main()
        {
            do
            {
                Console.WriteLine("The birth of heroes and villains!\nChoose type: human, elf, orc...\n");

                Console.Write("Hero type > ");
                string heroType = Console.ReadLine();

                if (String.IsNullOrEmpty(heroType))
                    break;

                HeroCreator heroFactory = Creator(heroType);

                if (heroFactory == null)
                {
                    Console.WriteLine("Invalid hero type! Try again!");
                    continue;
                }
                else
                {
                    Hero hero = heroFactory.HeroFactoryMethod();

                    if (hero == null)
                        Console.WriteLine("No more hero of this type...");
                    else
                        Console.WriteLine("New hero created: {0}", hero.Description());
                }
            }
            while (true);
        }
    }
}
