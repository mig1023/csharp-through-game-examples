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

