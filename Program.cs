using System;

namespace Covid
{
    class CovidProgram
    {
        static int[] cases = new int[] { 0, 4, 15, 30, 45, 60, 82, 110, 150, 193, 200, 205, 199, 170, 150, 153, 160, 200 };

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
                            // Den 1, p59pad 10, R=0
                            Console.WriteLine("Den {0} přibylo {1} případů, R = {2}", i + 1, cases[i], simpleR(i));
                            // Console.WriteLine("Den " + (i + 1) + " přibylo " + cases[i] + " případů, R = " + simpleR(i));
                        }
                        Console.WriteLine();
                        Console.WriteLine("Máme záznam z " + cases.Length + " dnů");
                        if (cases.Length > 0)
                        {
                            Console.WriteLine("Pokud R=0, chybí pro výpočet R údaje.");
                            Console.WriteLine("Průměr je " + casesPerDayAverage());
                            Console.WriteLine("Počet nakažených na 100 tisíc obyvatel je " + casesPer100K());
                            Console.WriteLine((minDay(out int minimum) + 1) + ".den bylo nalezeno minimum: " + minimum);
                            int dayx = maxDay();
                            Console.WriteLine(dayx + ".den" + " bylo nalezeno maximum: " + cases[dayx]);
                        }
                        break;
                    case 2:
                        Console.Write("Který den chcete upravit? ");
                        int day = readPositiveInteger();
                        if (day > cases.Length - 1)
                        {
                            Console.WriteLine("Chyné zadání, vyberte prosím existující záznam.");
                        }
                        else
                        { 
                            Console.Write("Zadejte počet případů. ");
                            int dayCases = readPositiveInteger();
                            cases[day - 1] = dayCases;
                            Console.WriteLine("Den " + day + " byl upraven.");
                        }
                        break;
                    case 3:
                        Array.Resize(ref cases, cases.Length + 1);
                        cases[cases.Length - 1] = readPositiveInteger();
                        Console.WriteLine("Byl přidán záznam.");
                        break;
                    case 4:
                        Array.Resize(ref cases, 0);
                        Console.WriteLine("Záznamy byly smazány.");
                        break;
                    case 5:
                        Console.WriteLine("Predikce vývoje");
                        break;
                    case 6:
                        printDaysRange();
                        break;
                    case 7:
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
            Console.WriteLine("6: Zobrazit záznam v rozsahu dnů");
            Console.WriteLine("7: Ukončit program");
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
            while (!tryReadInteger(out result) || result < 1 || result > 6)
            {
                Console.WriteLine();
                Console.WriteLine("Chybné zadání, napište platné číslo operace: ");
                printMenu();
            }
            return result;
        }

        static int readPositiveInteger()
        {
            int result;
            Console.WriteLine("Zadejte celé kladné číslo: ");
            while (!tryReadInteger(out result) || result < 0)
            {
                Console.WriteLine();
                Console.WriteLine("Chybné zadání, napište platné celé kladné číslo: ");
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
            return Math.Round(soucet / cases.Length, 2);
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
            for (int j = 1; j < cases.Length; j++)
            {
                if (cases[j] < minimum)
                {
                    minimum = cases[j];
                    day = j;
                }
            }
            return day;
        }
        static int maxDay()
        {
            int day = -1;
            int maximum = int.MinValue;
            for (int i = 0; i < cases.Length; i++)
            {
                if (cases[i] > maximum)
                {
                    maximum = cases[i];
                    day = i;
                }
            }
            return day;
        }
        static double simpleR(int day)
        {
            // R(i) = ( N(i) + N(i-1) + N(i-2) + N(i-3) + N(i-4) + N(i-5) + N(i-6) ) / ( N(i-5) + N(i-6) + N(i-7) + N(i-8) + N(i-9) + N(i-10) + N(i-11) )
            //cases[day] +
           if (day < 11)
            {
                return 0;
            }
         return  Math.Round(1.0 * casesPerPeriod(day - 6, day) / casesPerPeriod(day - 11, day - 5), 2);
        }
        static int casesPerPeriod(int firstDay, int lastDay) 
        {
            int soucetPripadu = 0;
            for (int i = firstDay ; i <= lastDay ; i++)
            {
                soucetPripadu = soucetPripadu + cases[i];
            }
            return soucetPripadu;
        }
        static void printDaysRange()
        {
            Console.WriteLine("Napište počáteční den: ");
            int firstDay = readDayNumber();
            Console.WriteLine("Napište koncový den: ");
            int lastDay = readDayNumber();
            for (int i = firstDay; i<=lastDay; i++)
            {
                Console.WriteLine("Den {0} přibylo {1} případů, R = {2}", i + 1, cases[i], simpleR(i));
            }
            return;

        }
        static int readDayNumber()
        {
            // ziskej cislo od uzivatele
            // kontrola uspesneho zadani
            // kontrola rozsahu
            // chyba -> opakuj zadani
            // v poradku -> vrat den
            int result;
            while (!tryReadInteger(out result) || result <1 || result > cases.Length)
            {
                Console.WriteLine();
                Console.WriteLine("Chybné zadání, napište existující den.");
            }
            return result-1;
        }
            
            
        
         
    }







} 
    


