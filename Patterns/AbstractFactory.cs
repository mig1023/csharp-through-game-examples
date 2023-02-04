using System;
using System.Collections.Generic;

namespace csharp_through_code_examples
{
    class Program
    {
        public class Hero
        {
            public string Name;
            protected int Hitpoints;
            protected string Information;

            public AbstractrDescription Description;

            public Hero(HeroFactory factory)
            {
                Name = factory.GetName();
                Hitpoints = factory.GetHitpoints();
                Information = factory.GetWeapon();
                Description = factory.SetDescription();
            }

            public string Discribe() => String.Format("{0} ({1} hitpoints), {2}{3}",
                Name, Hitpoints, Information, Description.Get());
        }

        public abstract class HeroFactory
        {
            public abstract string GetName();
            public abstract int GetHitpoints();
            public abstract string GetWeapon();
            public abstract AbstractrDescription SetDescription();
        }

        public abstract class AbstractrDescription
        {
            public abstract string Get();
        }

        public class GoodPersonage : AbstractrDescription
        {
            public override string Get() => ", this is good guy!";
        }

        public class BadPersonage : AbstractrDescription
        {
            public override string Get() => ", this is bad guy!";
        }

        public class HumanFactory : HeroFactory
        {
            private string weapon;
            private List<string> names = new List<string> { "Aragorn", "Boromir", "Faramir" };
            private static int nameIndex = -1;

            public HumanFactory(string hisWeapon) => weapon = hisWeapon;

            public override string GetName()
            {
                nameIndex += 1;
                return nameIndex >= names.Count ? String.Empty : names[nameIndex];
            }

            public override int GetHitpoints() => 10;
            public override string GetWeapon() => String.Format("with his beautiful {0}", weapon);
            public override AbstractrDescription SetDescription() => new GoodPersonage();
        }

        public class OrcFactory : HeroFactory
        {
            private string weapon;
            private List<string> names = new List<string> { "Azog", "Ugluk", "Grishnakh" };
            private static int nameIndex = -1;

            public OrcFactory(string hisWeapon) => weapon = hisWeapon;

            public override string GetName()
            {
                nameIndex += 1;
                return nameIndex >= names.Count ? String.Empty : names[nameIndex];
            }

            public override int GetHitpoints() => 12;
            public override string GetWeapon() => String.Format("with his hideous {0}", weapon);
            public override AbstractrDescription SetDescription() => new BadPersonage();
        }

        public static HeroFactory GetFactory(string type, string weapon)
        {
            switch (type.ToLower())
            {
                case "human":
                    return new HumanFactory(weapon);

                case "orc":
                    return new OrcFactory(weapon);

                default:
                    return null;
            }
        }

        static void Main()
        {
            Console.WriteLine("Another birth of heroes and villains!\nChoose type: human or orc...\n");

            do
            {
                Console.Write("Hero type  > ");
                string heroType = Console.ReadLine();

                Console.Write("His weapon > ");
                string heroWeapon = Console.ReadLine();

                if (String.IsNullOrEmpty(heroType) || String.IsNullOrEmpty(heroWeapon))
                    break;

                HeroFactory heroFactory = GetFactory(heroType, heroWeapon);

                if (heroFactory == null)
                {
                    Console.WriteLine("Invalid hero type! Try again!");
                    continue;
                }

                Hero hero = new Hero(heroFactory);

                if (String.IsNullOrEmpty(hero.Name))
                {
                    Console.WriteLine("No more hero of this type...");
                    continue;
                }

                Console.WriteLine(hero.Discribe());
            }
            while (true);
        }
    }
}
