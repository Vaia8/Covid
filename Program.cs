using System;

namespace Covid
{
    class CovidProgram
    {
        static int[] cases = new int[] { 1, 4, 15, 30, 45, 60, 82, 110, 150, 193 };

        static void Main(string[] args)
        {
            while (true)
            {
                int operation = readOperation();

                switch (operation)
                {
                    case 1:
                        Console.WriteLine();
                        for (int i = 0; i < cases.Length; i = i + 1)
                        {
                            Console.WriteLine("Den " + (i + 1) + " přibylo " + cases[i] + " případů");
                        }
                        Console.WriteLine();
                        Console.WriteLine("Máme záznam z " + cases.Length + " dnů");
                        if (cases.Length > 0)
                        {  
                            Console.WriteLine("Průměr je " + casesPerDayAverage());
                            Console.WriteLine("Počet nakažených na 100 tisíc obyvatel je " + casesPer100K());
                            Console.WriteLine((minDay(out int minimum) + 1) + ".den bylo nalezeno minimum: " + minimum);
                            // Ukol
                            // 1) Napsat funkci int maxDay()
                        }
                        break;
                    case 2:
                        //zeptám se na den který chce upravit
                        Console.Write("Který den chcete upravit? ");
                        //uložím zadné číslo do promenné typu int
                        int day = readInteger();
                        //zeptam se na pocet pripadů které chce do upravovaneho dne zapsat
                        Console.Write("Zadejte počet případů. ");
                        //ulozim do dne novy pocet
                        int dayCases = readInteger();
                        cases[day - 1] = dayCases;
                        //potvrdim ze operace probehla
                        Console.WriteLine("Den " + day + " byl upraven.");
                        break;
                    case 3:
                        Array.Resize(ref cases, cases.Length + 1);
                        cases[cases.Length - 1] = readInteger();
                        Console.WriteLine("Byl přidán záznam.");
                        break;
                    case 4:
                        Array.Resize(ref cases, 0);
                        Console.WriteLine("Záznamy byly smazány.");
                        break;
                    case 5:
                        break;
                    case 6:
                        System.Environment.Exit(0);
                        break;
                }
            }
        }
        static void printMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Pro provedení operace napište číslo operace");
            Console.WriteLine();
            Console.WriteLine("1: Zobrazit záznam případů");
            Console.WriteLine("2: Upravit záznam");
            Console.WriteLine("3: Přidat záznam");
            Console.WriteLine("4: Smazat záznam");
            Console.WriteLine("5: Zobrazit predikci vývoje");
            Console.WriteLine("6: Ukončit program");
            Console.WriteLine();
        }

        /*
        * Přečte hodnotu zadanou uživatelem. 
        * Vrátí TRUE, zadal-li uživatel platné celé číslo. 
        * Vrátí FALSE, pokud zadanou hodnotu nelze na celé číslo převést.
        */
        static bool tryReadInteger(out int result)
        {
            string s = Console.ReadLine();
            return int.TryParse(s, out result);
        }

        static int readOperation()
        {
            int result;
            printMenu();
            while (!tryReadInteger(out result))
            {
                Console.WriteLine("Chybné zadání, napište platné číslo operace: ");
                printMenu();
            }
            return result;
        }

        static int readInteger()
        {
            int result;
            Console.WriteLine("Zadejte celé číslo: ");
            while (!tryReadInteger(out result)) 
             {
                Console.WriteLine("Chybné zadání, napište platné celé číslo: ");
             }
            return result;
        }
        static double casesPerDayAverage()
        {
            double soucet = 0;
            for (int i = 0; i < cases.Length; i = i + 1)
            {
                soucet = soucet + cases[i];
            }
            return soucet/cases.Length;
        }
        static double casesPer100K()
        {
           double soucet = 0;
           for (int i = 0; i < cases.Length; i = i + 1)
            {
                soucet = soucet + cases[i];
            }
            return soucet * 100000 / 10000000;
        }
        static int minDay(out int minimum)
        {
            int day = -1;
            minimum = int.MaxValue;
            for (int j=0; j < cases.Length; j++)
            {
                if (cases[j] < minimum)
                {
                   minimum = cases[j];
                   day = j;
                }
            }
            return day;
        }
    }
}
