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
