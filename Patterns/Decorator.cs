using System;

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

            var fullPhrase = phrases[random.Next(phrases.Count)];

            var puzzle = fullPhrase
                .Split(' ')
                .Select(x => x.Trim())
                .OrderBy(a => random.Next())
                .ToList();

            Console.WriteLine("Puzzles:");

            for (int i = 0; i < puzzle.Count(); i++)
                Console.WriteLine("{0}: {1}", i + 1, puzzle[i]);

            var responseLine = String.Empty;

            do
            {
                Console.Write("\nEnter your order (1 2 3 4...): ");

                responseLine = Console.ReadLine();

                if (String.IsNullOrEmpty(responseLine))
                    break;

                var responseStrings = responseLine.Split(' ').ToList();

                if (responseStrings.Count() < puzzle.Count)
                {
                    Console.WriteLine("Fail! Not enough answers! Try again!");
                    continue;
                }

                var response = responseStrings.Select(x => int.Parse(x)).ToList();

                var newPhrase = new Component();
                Decorator newDecorator = null;
                Decorator prevDecorator = null;

                foreach (int word in response)
                {
                    newDecorator = new Decorator(puzzle[word - 1]);
                    newDecorator.SetComponent(prevDecorator ?? (ComponentAbstraction)newPhrase);
                    prevDecorator = newDecorator;
                }

                var responsePhrase = newDecorator.Operation().Trim();

                Console.WriteLine("Result: {0}", responsePhrase);

                if (fullPhrase == responsePhrase)
                {
                    Console.WriteLine("WIN!");
                    responseLine = String.Empty;
                }
                else
                {
                    Console.WriteLine("Fail! Try again!");
                }
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
