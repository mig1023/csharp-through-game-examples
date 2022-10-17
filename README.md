# С# through game examples

## Content

0. [Hello World](#hello-world)
1. [Variables types](#variables-types)
2. [List](#list)
3. [Delegate](#delegate)
4. [Class Encapsulation](#class-encapsulation)
5. [Class Inheritance](#class-inheritance)
6. [Class Dispose](#class-dispose)
7. [Interfaces](#interfaces)
8. [Strategy pattern](#strategy-pattern)
9. [Observer pattern](#observer-pattern)
10. [Decorator pattern](#decorator-pattern)
11. [Factory method pattern](#factory-method-pattern)
12. [Abstract factory pattern](#abstract-factory-pattern)

## Hello World

```c#
using System;

namespace hello_world
{
    class Program
    {
        private static Random rand = new Random();

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World! It's guessing game!\n");

            int secret = rand.Next(100) + 1;
            int attempt = 0;
            int attemptsNumber = 0;

            do
            {
                attemptsNumber += 1;

                Console.Write(String.Format("Your {0}try: ",
                    (attemptsNumber > 1 ? "next " : String.Empty)));

                bool thisIsNumber = int.TryParse(Console.ReadLine(), out attempt);

                if (!thisIsNumber)
                    Console.WriteLine("It doesn't look like a number...\n");

                else if (attempt > secret)
                    Console.WriteLine("Overshoot!\n");

                else if (attempt < secret)
                    Console.WriteLine("Undershoot!\n");

                else
                    Console.WriteLine(String.Format("Exactly! You WIN!\n\n" +
                        "Number of attempts: {0}",attemptsNumber));
            }
            while (secret != attempt);

            Console.ReadLine();
        }
    }
}
```
## Variables types

```c#
namespace csharp_through_code_examples
{
    class Program
    {
        private static Random random = new Random();

        private static Dictionary<string, List<string>> AllTypes = new Dictionary<string, List<string>>
        {
            ["sbyte"]   = new List<string> { "-128", "127" },
            ["byte"]    = new List<string> { "0", "255" },
            ["short"]   = new List<string> { "-32768", "32767" },
            ["int"]     = new List<string> { "-2147483648", "2147483647" },
            ["uint"]    = new List<string> { "0", "4294967295" },
            ["ushort"]  = new List<string> { "0", "65535" },
            ["ulong"]   = new List<string> { "0", "18446744073709551615" },
            ["long"]    = new List<string> { "-9223372036854775808", "9223372036854775807" },
            ["bool"]    = new List<string> { "true", "false" },
            ["string"]  = new List<string> { "abcdefg", "foo bar" },
            ["char"]    = new List<string> { "a", "\\0", "\\u006A" },
        };

        private static string Variants(string correct)
        {
            List<string> types = new List<string> { correct };

            for (int i = 1; i < 3; i++)
            {
                string nextVariant;

                do
                {
                    nextVariant = AllTypes.ElementAt(random.Next(AllTypes.Count)).Key;
                }
                while (types.Contains(nextVariant));

                types.Add(nextVariant);
            }

            List<string> randTypes = types.OrderBy(x => random.Next()).ToList();

            return String.Format("{0}, {1} or {2}?", randTypes[0], randTypes[1], randTypes[2]);
        }

        static void Main(string[] args)
        {
            int win = 0, round = 0;

            Console.WriteLine("Try to guess the type of the variable by value!\n");

            while (round < 5)
            {
                string test = AllTypes.ElementAt(random.Next(AllTypes.Count)).Key;
                string testLine = AllTypes[test][random.Next(AllTypes[test].Count)];

                Console.Write(String.Format("Var: {0}\nThis is: {1}\n> ",
                    testLine, Variants(test)));

                string response = Console.ReadLine();

                if (response == test)
                {
                    Console.WriteLine("YES!\n");
                    win += 1;
                }
                else if (AllTypes.ContainsKey(response) && AllTypes[response].Contains(testLine))
                {
                    Console.WriteLine("Ow... But why not?\n");
                    win += 1;
                }
                else
                    Console.WriteLine(String.Format("No ({0})\n", test));

                round += 1;
            }

            Console.WriteLine(String.Format("Result: {0}/5", win));
            Console.ReadLine();
        }
    }
}
```

## List

```c#
using System;
using System.Collections.Generic;

namespace csharp_through_code_examples
{
    class Program
    {
        private static Random random = new Random();

        private static List<string> SecretWords = new List<string>
        {
            "antelope", "bear", "cow", "donkey", "falcon", "gibbon", "horse",
            "iguana", "jaguar", "koala", "lion", "monkey", "octopus", "shark"
        };

        private static List<char> Opened = new List<char>();

        private static string ShowWord(string word)
        {
            for (int i = 0; i < word.Length; i++)
                if (!Opened.Contains(word[i]))
                    word = word.Replace(word[i], '*');

            return word;
        }

        private static bool WordOpened(string word) => !ShowWord(word).Contains("*");

        static void Main(string[] args)
        {
            Console.WriteLine("Try to guess the word!\n");

            string word = SecretWords[random.Next(SecretWords.Count)];
            int attempts = 0;

            do
            {
                Console.Write(String.Format("Word: {0}\n{1}etter: ",
                    ShowWord(word), (attempts > 0 ? "Next l" : "L")));
                Char key = Char.ToLower(Console.ReadKey().KeyChar);

                if (word.Contains(key))
                {
                    Console.WriteLine("\nYES!\n");
                    Opened.Add(key);
                }
                else
                    Console.WriteLine("\nNo...\n");

                attempts += 1;
            }
            while (!WordOpened(word));

            Console.WriteLine(String.Format("You WIN!\nAttempts: {0}", attempts));
            Console.ReadLine();
        }
    }
}
```

## Delegate

```c#
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

        static void Main(string[] args)
        {
            Console.WriteLine("Try to hit the enemy!\n");

            while(true)
            {
                Console.Write("How many hits > ");

                bool success = int.TryParse(Console.ReadLine(), out int hits);

                if (!success || (hits <= 0))
                    break;

                EnemyScreams scream = null;
                string enemyName = String.Empty;
                    
                if (random.Next(100) % 2 == 0)
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
```

## Class Encapsulation 

```c#
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

        static void Main(string[] args)
        {
            Goblin goblin = new Goblin
            {
                Hitpoints = rand.Next(20) + 10,
            };

            Console.WriteLine(String.Format("You have to fight the {0} ({1} hitpoints)!\n" +
                "To hit him you have to type 'hit'!\n", goblin.Name(), goblin.Hitpoints));

            while (goblin.Hitpoints > 0)
            {
                Console.Write("> ");

                string action = Console.ReadLine();

                if (action.ToLower() == "hit")
                {
                    goblin.Wound();

                    Console.WriteLine(String.Format("You hit him! His hitponts dropped to {0}\n",
                        goblin.Hitpoints));
                }
                else
                    Console.WriteLine("I did not understand you...\n");
            }

            Console.WriteLine("YOU WON THIS BATTLE! Not surprisingly, but still...");
            Console.ReadLine();
        }
    }
}
```

## Class Inheritance

```c#
using System;

namespace csharp_through_code_examples
{
    class Program
    {
        private static Random rand = new Random();

        private class Сharacter
        {
            public int Hitpoints { get; set; }

            public string Name { get; set; }

            public void Wound() => Hitpoints -= rand.Next(5) + 1;
        }

        private class Hero : Сharacter
        {
            public string BeautifulSpeeches() => "Oh, it hurts, but I won't give up!";
        }

        private class Goblin : Сharacter
        {
            public string Swearing() => "Oh, *$?@#&!! I hate this %#@*!!";
        }

        static void Main(string[] args)
        {
            Hero hero = new Hero
            {
                Name = "You",
                Hitpoints = rand.Next(20) + 10,
            };

            Goblin goblin = new Goblin
            {
                Name = "Ugly goblin",
                Hitpoints = rand.Next(20) + 10,
            };

            Console.WriteLine(String.Format("You have {0} hitpoints and gotta fight the {1} ({2} hitpoints)!\n" +
                "To hit him you have to type 'attack'!\n", hero.Hitpoints, goblin.Name, goblin.Hitpoints));

            while ((goblin.Hitpoints > 0) && (hero.Hitpoints > 0))
            {
                Console.Write("> ");

                string action = Console.ReadLine();

                if (action.ToLower() == "attack")
                {
                    goblin.Wound();

                    Console.WriteLine(String.Format("You hit him! His hitponts dropped to {0}",
                        goblin.Hitpoints));

                    if (goblin.Hitpoints > 0)
                        Console.WriteLine(String.Format("{0}: {1}\n", goblin.Name, goblin.Swearing()));

                    hero.Wound();

                    Console.WriteLine(String.Format("{0} hit you! Your hitponts dropped to {1}",
                        goblin.Name, hero.Hitpoints));

                    if (hero.Hitpoints > 0)
                        Console.WriteLine(String.Format("{0}: {1}\n", hero.Name, hero.BeautifulSpeeches()));
                }
                else
                    Console.WriteLine("I did not understand you...\n");
            }

            if (goblin.Hitpoints < 0)
                Console.WriteLine("YOU WON THIS BATTLE!");
            else
                Console.WriteLine("You lost this fight...");

            Console.ReadLine();
        }
    }
}
```

## Class Dispose

```c#
using System;

namespace csharp_through_code_examples
{
    class Program
    {
        private class Gandalf : IDisposable
        {
            public Gandalf() => Console.WriteLine("Gandalf was born in 2500 of the Third Age!");

            public void Dispose() => Console.WriteLine("Hey, Gandalf, you have to go!");

            ~Gandalf() => Console.WriteLine("Gandalf sailed away at 3200 of the Third Age!");
        }

        static void GandalfHistory()
        {
            Gandalf gandalf = new Gandalf();

            while (true)
            {
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    gandalf.Dispose();
                    break;
                }
                else
                    Console.WriteLine("Gandalf does something good!");
            }
        }

        static void Main(string[] args)
        {
            GandalfHistory();

            Console.WriteLine("This is end of the Gandalf story!");

            GC.Collect();

            Console.ReadLine();
        }
    }
}
```

## Interfaces

```c#
using System;

namespace csharp_through_code_examples
{
    class Program
    {
        private static Random rand = new Random();
        interface Movement
        {
            string Sound();
            int Move();
        }

        private class Foot : Movement
        {
            public string Sound() => "Step! Step! Step!";
            public int Move() => 1;
        }

        private class Velo : Movement
        {
            public string Sound() => "Pedals turn! Turn! Turn!";
            public int Move() => 4;
        }

        private class Car : Movement
        {
            public string Sound() => "On the highway whoosh!";
            public int Move() => 25;
        }

        private class Helicopter : Movement
        {
            public string Sound() => "Helicopter propeller whoooosh!";
            public int Move() => 300;
        }

        private class Airplane : Movement
        {
            public string Sound() => "Airport is left behind...";
            public int Move() => 1000;
        }

        private static Movement Transport(string type)
        {
            switch (type.ToLower())
            {
                case "velo":
                    return new Velo();

                case "car":
                    return new Car();

                case "helicopter":
                    return new Helicopter();

                case "airplane":
                    return new Airplane();

                case "foot":
                    return new Foot();

                default:
                    return null;
            }
        }

        static void Main(string[] args)
        {
            int distance = rand.Next(500, 3000);

            Console.WriteLine(String.Format("You have to overcome the distance of {0} km!\n" +
                "Make a choice: airplane, helicopter, car, velo, foot?", distance));

            int steps = 0;

            while (true)
            {
                steps += 1;

                Console.Write("\n> ");

                Movement movement = Transport(Console.ReadLine());

                if (movement == null)
                    continue;

                int step = movement.Move();

                distance -= step;

                Console.WriteLine(movement.Sound());
                Console.WriteLine(String.Format("You have covered {0} km!", step));

                if (distance < 0)
                {
                    distance = Math.Abs(distance);
                    Console.WriteLine(String.Format("Overmuch! You need to go back {0} km!", distance));
                }
                else if (distance == 0)
                {
                    Console.WriteLine(String.Format("WIN! it took you {0} steps!", steps));
                    break;
                }
                else
                {
                    Console.WriteLine(String.Format("{0} km left.", distance));
                }
            }

            Console.ReadLine();
        }
    }
}
```

## Strategy pattern

```c#
using System;

namespace csharp_through_code_examples
{
    interface WeaponBehavior
    {
        int Use();
        string Name();
    }

    class Character
    {
        public WeaponBehavior Weapon;
        public string Name;
        public int Hitpoints;
    }

    class SwordBehavior : WeaponBehavior
    {
        public int Use() => 5;
        public string Name() => "sword";
    }

    class AxeBehavior : WeaponBehavior
    {
        public int Use() => 4;
        public string Name() => "axe";
    }

    class СlubBehavior : WeaponBehavior
    {
        public int Use() => 3;
        public string Name() => "club";
    }

    class WarhammerBehavior : WeaponBehavior
    {
        public int Use() => 6;
        public string Name() => "warhammer";
    }

    class KnifeBehavior : WeaponBehavior
    {
        public int Use() => 2;
        public string Name() => "knife";
    }

    class Program
    {
        static Character hero = new Character
        {
            Name = "Hero",
            Hitpoints = 15,
            Weapon = new SwordBehavior(),
        };

        static Character orc = new Character
        {
            Name = "Orc",
            Hitpoints = 15,
            Weapon = new СlubBehavior(),
        };

        static void Fight(Character whoAttack, Character beingAttacked)
        {
            if (whoAttack.Hitpoints <= 0)
                return;

            beingAttacked.Hitpoints -= whoAttack.Weapon.Use();

            Console.WriteLine("{0} use {1}, {2} lost {3} hitpoints!",
                whoAttack.Name, whoAttack.Weapon.Name(), beingAttacked.Name, whoAttack.Weapon.Use());
        }

        static WeaponBehavior ChangeWeapon()
        {
            Random newWeapon = new Random();

            switch (newWeapon.Next(5))
            {
                default:
                case 0:
                    return new SwordBehavior();

                case 1:
                    return new AxeBehavior();

                case 2:
                    return new СlubBehavior();

                case 3:
                    return new WarhammerBehavior();

                case 4:
                    return new KnifeBehavior();
            }
        }

        static void Main(string[] args)
        {
            while ((hero.Hitpoints > 0) && (orc.Hitpoints > 0))
            {
                hero.Weapon = ChangeWeapon();
                orc.Weapon = ChangeWeapon();

                Fight(hero, orc);
                Fight(orc, hero);
            }

            Console.WriteLine("\n{0} WIN!!", orc.Hitpoints <= 0 ? hero.Name : orc.Name);
        }
    }
}
```

## Observer pattern

```c#
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
        static void Main(string[] args)
        {
            Hero hero = new Hero();

            Orc orc = new Orc(hitpoints: 30);
            Goblin goblin = new Goblin(hitpoints: 20);

            hero.Register(enemy: orc);
            hero.Register(enemy: goblin);

            string action = String.Empty;

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
```

## Decorator pattern

```c#
using System;
using System.Collections.Generic;
using System.Linq;

namespace csharp_through_code_examples
{
    class Program
    {
        private static Random random = new Random();

        private static List<string> phrases = new List<string>
        {
            "The journey of a thousand miles begins with one step",
            "That which does not kill us makes us stronger",
            "I want to believe",
            "The secret of getting ahead is getting started",
        };

        static void Main(string[] args)
        {
            string fullPhrase = phrases[random.Next(phrases.Count)];

            List<string> puzzle = fullPhrase
                .Split(' ')
                .Select(x => x.Trim())
                .OrderBy(a => random.Next())
                .ToList();

            Console.WriteLine("Puzzles:");

            for (int i = 0; i < puzzle.Count(); i++)
                Console.WriteLine("{0}: {1}", i + 1, puzzle[i]);

            string responseLine = String.Empty;

            do
            {
                Console.Write("\nEnter your order (1 2 3 4...): ");

                responseLine = Console.ReadLine();

                if (String.IsNullOrEmpty(responseLine))
                    break;

                List<string> responseStrings = responseLine.Split(' ').ToList();

                if (responseStrings.Count() < puzzle.Count)
                {
                    Console.WriteLine("Fail! Not enough answers! Try again!");
                    continue;
                }

                List<int> response = responseStrings.Select(x => int.Parse(x)).ToList();

                Component newPhrase = new Component();
                Decorator newDecorator = null, prevDecorator = null;

                foreach (int word in response)
                {
                    newDecorator = new Decorator(puzzle[word - 1]);
                    newDecorator.SetComponent(prevDecorator ?? (ComponentAbstraction)newPhrase);
                    prevDecorator = newDecorator;
                }

                string responsePhrase = newDecorator.Operation().Trim();

                Console.WriteLine("Result: {0}", responsePhrase);

                if (fullPhrase == responsePhrase)
                {
                    Console.WriteLine("WIN!");
                    responseLine = String.Empty;
                }
                else
                    Console.WriteLine("Fail! Try again!");
            }
            while (!String.IsNullOrEmpty(responseLine));
        }
    }

    abstract class ComponentAbstraction
    {
        public abstract string Operation();
    }

    class Component : ComponentAbstraction
    {
        public override string Operation() =>
            String.Empty;
    }

    abstract class DecoratorAbstraction : ComponentAbstraction
    {
        protected ComponentAbstraction component;

        public void SetComponent(ComponentAbstraction component) =>
            this.component = component;

        public override string Operation() =>
            component.Operation();
    }

    class Decorator : DecoratorAbstraction
    {
        private string Word;

        public Decorator(string word) =>
            Word = word;

        public override string Operation() =>
            String.Format("{0} {1}", base.Operation(), Word);
    }
}
```

## Factory method pattern

```c#
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

        static void Main(string[] args)
        {
            do
            {
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
```

## Abstract factory pattern

```c#
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

        static void Main(string[] args)
        {
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
```
