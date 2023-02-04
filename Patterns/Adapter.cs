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
