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
            {
                if (!Opened.Contains(word[i]))
                    word = word.Replace(word[i], '*');
            }

            return word;
        }

        private static bool WordOpened(string word) =>
            !ShowWord(word).Contains("*");

        static void Main()
        {
            Console.WriteLine("Try to guess the word!\n");

            var word = SecretWords[random.Next(SecretWords.Count)];
            var attempts = 0;

            do
            {
                Console.Write("Word: {0}\n{1}etter: ", ShowWord(word), (attempts > 0 ? "Next l" : "L"));

                var key = Char.ToLower(Console.ReadKey().KeyChar);

                if (word.Contains(key))
                {
                    Console.WriteLine("\nYES!\n");
                    Opened.Add(key);
                }
                else
                {
                    Console.WriteLine("\nNo...\n");
                }
                    
                attempts += 1;
            }
            while (!WordOpened(word));

            Console.WriteLine("You WIN!\nAttempts: {0}", attempts);
            Console.ReadLine();
        }
    }
}
