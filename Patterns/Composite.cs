using System;

class Program
{
    abstract class Line
    {
        protected string Name;

        public Directory Parent { get; set; }

        public Line(string name) =>
            Name = name;

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
            Console.WriteLine(new String(' ', nesting) + Name);

            foreach (Line component in children)
                component.Display(nesting + 2);
        }
    }

    class File : Line
    {
        public File(string name) : base(name) { }

        public override void Display(int nesting) =>
            Console.WriteLine(new String(' ', nesting) + Name);
    }

    static void Main()
    {
        var files = 0;
        var filesInCurrent = 0;
        var nesting = 0;

        var rand = new Random();
        var root = new Directory("root");
        var current = root;
        var nestingLevel = new Dictionary<int, int>();

        Console.Write("Nesting level game!\n\nDifficulty level (number of files or folders)? ");
        var difficultyOk = int.TryParse(Console.ReadLine(), out int difficulty);

        if (!difficultyOk)
            difficulty = 10;

        for (int i = 0; i < difficulty; i++)
        {
            var action = rand.Next(3);

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
            var file = rand.Next(files) + 1;

            do
            {
                Console.Write("\nWhat is the nesting level of file {0}? ", file);

                var responseLine = Console.ReadLine();

                if (String.IsNullOrEmpty(responseLine))
                    Environment.Exit(0);

                var responseOk = int.TryParse(responseLine, out int response);

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

            }
            while (true);
        }
    }
}
