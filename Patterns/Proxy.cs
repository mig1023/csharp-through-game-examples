using System;
using System.Collections.Generic;

class Program
{
    static List<Pazzle> Pazzles = new List<Pazzle>();

    abstract class Pazzle
    {
        protected bool IsOpened { get; set; } 
        abstract public void Open();
        abstract public bool IsOpen();
        abstract public int GetNear();
    }

    class EmptyPazzle : Pazzle
    {
        private int Near { get; set; }

        public EmptyPazzle(int near)
        {
            IsOpened = false;
            Near = near;
        }

        public override void Open() => IsOpened = true;
        public override bool IsOpen() => IsOpened;
        public override int GetNear() => Near;
    }

    class Bomb : Pazzle
    {
        EmptyPazzle emptyPazzle;

        public Bomb(int near) => emptyPazzle = new EmptyPazzle(near);

        public override void Open()
        {
            Console.WriteLine("\n BOOOOOOOM!!!!!!");
            emptyPazzle.Open();
        }

        public override bool IsOpen() => emptyPazzle.IsOpen();

        public override int GetNear() => emptyPazzle.GetNear();
    }

    private static void Show()
    {
        Console.WriteLine(" 1 -- 2 -- 3 -- 4 -- 5 -- 6 -- 7 -- 8");

        foreach (Pazzle Pazzle in Pazzles)
        {
            int near = Pazzle.GetNear();

            string symbol;

            if (!Pazzle.IsOpen())
                symbol = " X   ";
            else if (near == 2)
                symbol = "BOMB ";
            else
                symbol = String.Format(" {0}   ", near);

            Console.Write(symbol);
        }
        Console.WriteLine();
    }

    static void Main()
    {
        Random rand = new Random();
        int bombPosition = rand.Next(8);

        Console.WriteLine("Mini Minesweeper\n");

        for (int i = 0; i < 8; i++)
        {
            if (i == bombPosition)
                Pazzles.Add(new Bomb(2));
            else
            {
                int near = (i == bombPosition + 1) || (i == bombPosition - 1) ? 1 : 0;
                Pazzles.Add(new EmptyPazzle(near));
            }
        }

        int newPazzle;
        int openedPazzles = 0;

        do
        {
            Show();

            Console.Write("\n > ");
            string action = Console.ReadLine();

            if (String.IsNullOrEmpty(action))
            {
                break;
            }
            else if (!int.TryParse(action, out newPazzle) || (newPazzle > 8))
            {
                Console.WriteLine(" I did not understand you...");
            }
            else
            {
                Pazzles[newPazzle - 1].Open();
                openedPazzles += 1;

                bool fail = newPazzle == (bombPosition + 1);

                if (fail || (openedPazzles == 7))
                {
                    Console.WriteLine(fail ? "\n Fail..." : "\n WIN!!");
                    Show();
                    break;
                }
            }
        }
        while (true);
    }
}
