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
