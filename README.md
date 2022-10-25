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
13. [Singleton pattern](#singleton-pattern)
14. [Command pattern](#command-pattern)
15. [Adapter pattern](#adapter-pattern)
16. [Facade pattern](#facade-pattern)
17. [Template method pattern](#template-method-pattern)
18. [Iterator pattern](#iterator-pattern)
19. [Composite pattern](#composite-pattern)

## Hello World

```c#
using System;

namespace hello_world
{
    class Program
    {
        private static Random rand = new Random();

        static void Main()
        {
            Console.WriteLine("Hello World!\nIt's guessing game!\n");

            int secret = rand.Next(100) + 1;
            int attempt = 0;
            int attemptsNumber = 0;

            do
            {
                attemptsNumber += 1;

                Console.Write("Your {0}try: ", attemptsNumber > 1 ? "next " : String.Empty);

                bool thisIsNumber = int.TryParse(Console.ReadLine(), out attempt);

                if (!thisIsNumber)
                    Console.WriteLine("It doesn't look like a number...\n");

                else if (attempt > secret)
                    Console.WriteLine("Overshoot!\n");

                else if (attempt < secret)
                    Console.WriteLine("Undershoot!\n");

                else
                    Console.WriteLine("Exactly! You WIN!\n\nNumber of attempts: {0}", attemptsNumber);
            }
            while (secret != attempt);

            Console.ReadLine();
        }
    }
}
```
## Variables types

```c#
using System;
using System.Collections.Generic;
using System.Linq;

namespace csharp_through_code_examples
{
    class Program
    {
        private static Random random = new Random();

        private static Dictionary<string, List<string>> AllTypes = new Dictionary<string, List<string>>
        {
            ["sbyte"] = new List<string> { "-128", "127" },
            ["byte"] = new List<string> { "0", "255" },
            ["short"] = new List<string> { "-32768", "32767" },
            ["int"] = new List<string> { "-2147483648", "2147483647" },
            ["uint"] = new List<string> { "0", "4294967295" },
            ["ushort"] = new List<string> { "0", "65535" },
            ["ulong"] = new List<string> { "0", "18446744073709551615" },
            ["long"] = new List<string> { "-9223372036854775808", "9223372036854775807" },
            ["bool"] = new List<string> { "true", "false" },
            ["string"] = new List<string> { "abcdefg", "foo bar" },
            ["char"] = new List<string> { "a", "\\0", "\\u006A" },
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

        static void Main()
        {
            int win = 0, round = 0;

            Console.WriteLine("Try to guess the type of the variable by value!\n");

            while (round < 5)
            {
                string test = AllTypes.ElementAt(random.Next(AllTypes.Count)).Key;
                string testLine = AllTypes[test][random.Next(AllTypes[test].Count)];

                Console.Write("Var: {0}\nThis is: {1}\n> ", testLine, Variants(test));

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
                    Console.WriteLine("No ({0})\n", test);

                round += 1;
            }

            Console.WriteLine("Result: {0}/5", win);
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

        private static bool WordOpened(string word) =>
            !ShowWord(word).Contains("*");

        static void Main()
        {
            Console.WriteLine("Try to guess the word!\n");

            string word = SecretWords[random.Next(SecretWords.Count)];
            int attempts = 0;

            do
            {
                Console.Write("Word: {0}\n{1}etter: ", ShowWord(word), (attempts > 0 ? "Next l" : "L"));

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

            Console.WriteLine("You WIN!\nAttempts: {0}", attempts);
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

        static void Main()
        {
            Console.WriteLine("Try to hit the enemy!\n");

            while (true)
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

        static void Main()
        {
            Goblin goblin = new Goblin
            {
                Hitpoints = rand.Next(20) + 10,
            };

            Console.WriteLine("You have to fight the {0} ({1} hitpoints)!\n" +
                "To hit him you have to type 'hit'!\n", goblin.Name(), goblin.Hitpoints);

            while (goblin.Hitpoints > 0)
            {
                Console.Write("> ");

                string action = Console.ReadLine();

                if (action.ToLower() == "hit")
                {
                    goblin.Wound();

                    Console.WriteLine("You hit him! His hitponts dropped to {0}\n",
                        goblin.Hitpoints);
                }
                else
                    Console.WriteLine("I did not understand you...\n");
            }

            Console.WriteLine("YOU WON THIS BATTLE!\nNot surprisingly, but still...");
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

        static void Main()
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

            Console.WriteLine("You have {0} hitpoints and gotta fight the {1} ({2} hitpoints)!\n" +
                "To hit him you have to type 'attack'!\n", hero.Hitpoints, goblin.Name, goblin.Hitpoints);

            while ((goblin.Hitpoints > 0) && (hero.Hitpoints > 0))
            {
                Console.Write("> ");

                string action = Console.ReadLine();

                if (action.ToLower() == "attack")
                {
                    goblin.Wound();

                    Console.WriteLine("You hit him! His hitponts dropped to {0}", goblin.Hitpoints);

                    if (goblin.Hitpoints <= 0)
                        continue;
                    
                    Console.WriteLine("{0}: {1}\n", goblin.Name, goblin.Swearing());

                    hero.Wound();

                    Console.WriteLine("{0} hit you! Your hitponts dropped to {1}",
                        goblin.Name, hero.Hitpoints);

                    if (hero.Hitpoints > 0)
                        Console.WriteLine("{0}: {1}\n", hero.Name, hero.BeautifulSpeeches());
                }
                else
                    Console.WriteLine("I did not understand you...\n");
            }

            if (goblin.Hitpoints <= 0)
                Console.WriteLine("\nYOU WON THIS BATTLE!");
            else
                Console.WriteLine("\nYou lost this fight...");

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

        static void Main()
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

        static void Main()
        {
            int distance = rand.Next(500, 3000);

            Console.WriteLine("You have to overcome the distance of {0} km!\n" +
                "Make a choice: airplane, helicopter, car, velo, foot?", distance);

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
                Console.WriteLine("You have covered {0} km!", step);

                if (distance < 0)
                {
                    distance = Math.Abs(distance);
                    Console.WriteLine("Overmuch! You need to go back {0} km!", distance);
                }
                else if (distance == 0)
                {
                    Console.WriteLine("WIN! it took you {0} steps!", steps);
                    break;
                }
                else
                {
                    Console.WriteLine("{0} km left.", distance);
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

        static void Main()
        {
            Console.WriteLine("This is an epic battle!\n");

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

        static void Main()
        {
            Console.WriteLine("Try to restore word order!\n");

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
```

## Singleton pattern

```c#
using System;

namespace csharp_through_code_examples
{
    class Program
    {

        public class Counter
        {
            private static Counter instance = new Counter();

            private Counter() { }

            public static Counter NewInstance() => instance;

            private int counter = 99;

            public int Get() => --instance.counter;
        }

        static void Main()
        {
            int bottles = 99;

            do
            {
                Console.WriteLine("{0} bottles of beer on " +
                    "the wall", bottles);
                Console.WriteLine("{0} bottles of beer!", bottles);
                Console.WriteLine("Take one down, pass it around", bottles);

                Counter bottleCounter = Counter.NewInstance();
                bottles = bottleCounter.Get();

                Console.WriteLine("{0} bottles of beer on the wall!", bottles);
                Console.ReadLine();
            }
            while (bottles > 1);

            Console.WriteLine("1 bottle of beer on the wall\n1 bottle of beer!");
            Console.WriteLine("Take one down, pass it around\nNo bottles of beer on the wall");
        }
    }
}
```

## Command pattern

```c#
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

```

## Adapter pattern

```c#
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    interface IGermanOfficer
    {
        public string Greeting();
        public string Name();
        public string GermanMilitaryRank();
    }

    class GermanOfficer : IGermanOfficer
    {
        private string name;
        private string rank;

        public GermanOfficer(string setName, string setRank)
        {
            name = setName;
            rank = setRank;
        }

        public string Greeting() => String.Format("Jawohl! Mein Name ist");

        public string Name() => name;

        public string GermanMilitaryRank() => rank;
    }

    class SovietOfficer
    {
        private string name;
        private string rank;
        private string fakeName;

        public SovietOfficer(string setName, string setRank, string setFakeName)
        {
            name = setName;
            rank = setRank;
            fakeName = setFakeName;
        }

        public string Greeting() => String.Format("Здравия желаю! Меня зовут");

        public string Name() => name;

        public string FakeName() => fakeName;

        public string SovietMilitaryRank() => rank;
    }

    class SpyDisguise : IGermanOfficer
    {
        SovietOfficer officer;

        public SpyDisguise(SovietOfficer setOfficer) => officer = setOfficer;

        public string Greeting() => String.Format("Jawohl! Mein Name ist");

        public string Name()
        {
            string newName = officer.FakeName();

            string[] russianAlphabet =
                ("А Б В Г Д Е Ё Ж З И Й К Л М Н О П Р С Т У Ф Х Ц Ч Ш Щ Ы Э Ю Я " +
                "а б в г д е ё ж з и й к л м н о п р с т у ф х ц ч ш щ ы э ю я").Split(" ");

            string[] englishAlphabet =
                ("A B V G D E Yo Zh Z I Y K L M N O P R S T U F Kh Ts Ch Sh Shch Y E Yu Ya " +
                "a b v g d e yo zh z i y k l m n o p r s t u f kh ts ch sh shch y e yu ya").Split(" ");

            for (int i = 0; i <= 60; i++)
                newName = newName.Replace(russianAlphabet[i], englishAlphabet[i]);

            return newName;
        }
        public string GermanMilitaryRank() => "Standartenführer";
    }

    static void Main()
    {
        Console.WriteLine("Search for a Soviet spy!\n");

        Random rand = new Random();

        SovietOfficer sovietOfficer = new SovietOfficer("Максим Максимович Исаев", "полковник", "Штирлиц");

        Console.WriteLine("Soviet spy: {0} {1} {2} (моя легенда: {3})",
            sovietOfficer.Greeting(),
            sovietOfficer.SovietMilitaryRank(),
            sovietOfficer.Name(),
            sovietOfficer.FakeName());

        List<IGermanOfficer> germanOfficers = new List<IGermanOfficer>();

        germanOfficers.Add(new GermanOfficer("Müller", "Gruppenführer"));
        germanOfficers.Add(new GermanOfficer("Schellenberg", "Brigadeführer"));
        germanOfficers.Add(new GermanOfficer("Bormann", "Reichsminister"));
        germanOfficers.Add(new GermanOfficer("Wolff", "Obergruppenführer"));
        germanOfficers.Add(new SpyDisguise(sovietOfficer));

        germanOfficers = germanOfficers.OrderBy(x => rand.Next()).ToList();

        Console.WriteLine("\nGerman officers:");

        int i = 0;

        foreach (IGermanOfficer germanOfficer in germanOfficers)
        {
            Console.WriteLine("{0}. {1} {2} {3}",
                ++i,
                germanOfficer.Greeting(),
                germanOfficer.GermanMilitaryRank(),
                germanOfficer.Name());
        }

        Console.WriteLine("\nWho is soviet spy?");

        do
        {
            string responseLine = Console.ReadLine();

            if (String.IsNullOrEmpty(responseLine))
                break;

            bool okResponse = int.TryParse(responseLine, out int response);

            if (!okResponse || (response > germanOfficers.Count) || (response < 1))
                Console.WriteLine("Invalid response! Try again!");

            else if (germanOfficers[response - 1].Name() == "Shtirlits")
            {
                Console.WriteLine("YES! You WIN!");
                break;
            }
            else
                Console.WriteLine("No. Try again!");
        }
        while (true);
    }
}
```

## Facade pattern

```c#
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
```

## Template method pattern

```c#
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
```

## Iterator pattern

```c#
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
```

## Composite pattern

```c#
using System;
using System.Collections.Generic;

class Program
{
    abstract class Line
    {
        protected string name;
        public Directory Parent;

        public Line(string name) =>
            this.name = name;

        public abstract void Display(int nesting);
    }

    class Directory : Line
    {
        private List<Line> children = new List<Line>();

        public Directory(string name) : base(name) { }

        public void Add(Line component)
        {
            component.Parent = this;
            children.Add(component);
        }

        public override void Display(int nesting)
        {
            Console.WriteLine(new String(' ', nesting) + name);

            foreach (Line component in children)
                component.Display(nesting + 2);
        }
    }

    class File : Line
    {
        public File(string name) : base(name) { }

        public override void Display(int nesting) =>
            Console.WriteLine(new String(' ', nesting) + name);
    }

    static void Main()
    {
        int files = 0, filesInCurrent = 0, nesting = 0;

        Random rand = new Random();
        Directory root = new Directory("root");
        Directory current = root;
        Dictionary<int, int> nestingLevel = new Dictionary<int, int>();

        Console.Write("Nesting level game!\n\nDifficulty level (number of files or folders)? ");
        bool difficultyOk = int.TryParse(Console.ReadLine(), out int difficulty);

        if (!difficultyOk)
            difficulty = 10;

        for (int i = 0; i < difficulty; i++)
        {
            int action = rand.Next(3);

            if (action == 0)
            {
                nesting += 1;
                filesInCurrent = 0;
                Directory newComposite = new Directory("Directory");
                current.Add(newComposite);
                current = newComposite;
            }
            else if (action == 1)
            {
                files += 1;
                filesInCurrent += 1;
                current.Add(new File(String.Format("File {0}", files)));
                nestingLevel.Add(files, nesting);
            }
            else if (current != root)
            {
                if (filesInCurrent == 0)
                    current.Add(new File("(empty)"));

                nesting -= 1;
                current = current.Parent;
            }
        }

        root.Display(1);

        for (int r = 0; r < 3; r++)
        {
            int file = rand.Next(files) + 1;

            do
            {
                Console.Write("\nWhat is the nesting level of file {0}? ", file);

                string responseLine = Console.ReadLine();

                if (String.IsNullOrEmpty(responseLine))
                    Environment.Exit(0);

                bool responseOk = int.TryParse(responseLine, out int response);

                if (!responseOk)
                {
                    Console.WriteLine("Invalid response!");
                }
                else if (response != nestingLevel[file])
                {
                    Console.WriteLine("Wrong response!");
                }
                else
                {
                    Console.WriteLine("WIN!");
                    break;
                }

            } while (true);
        }
    }
}
```
