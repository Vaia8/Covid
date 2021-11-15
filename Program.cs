using System;

namespace Covid
{
    class CovidProgram
    {
        static int[] cases = new int[] { 0, 4, 15, 30, 45, 60, 82, 110, 150, 193, 200, 205, 199, 170, 150, 153, 160, 200, 235, 250, 260};

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
                            Console.WriteLine("Den {0} přibylo {1} případů, R = {2}, incidence7 = {3}, incidence14 = {4} ", 
                                i + 1, cases[i], simpleR(i), incidence7(i), incidence14(i));
                        }
                        Console.WriteLine();
                        Console.WriteLine("Máme záznam z " + cases.Length + " dnů");
                        if (cases.Length > 0)
                        {
                            Console.WriteLine("Pokud je R nebo incidence 0, chybí pro výpočet údaje.");
                            printStats(0, cases.Length-1);
                        }
                        break;
                    case 2:
                        Console.Write("Který den chcete upravit? ");
                        int day = readPositiveInteger();
                        if (day > cases.Length - 1)
                        {
                            Console.WriteLine("Chybné zadání, vyberte prosím existující záznam.");
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
                        Console.WriteLine("Predikci vývoje jsem nestihla");
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
            Console.WriteLine("4: Smazat záznamy");
            Console.WriteLine("5: Zobrazit predikci vývoje");
            Console.WriteLine("6: Zobrazit záznam v rozsahu dnů");
            Console.WriteLine("7: Ukončit program");
            Console.WriteLine();
        }

        static bool tryReadInteger(out int result)
        {
            string s = Console.ReadLine();
            return int.TryParse(s, out result);
        }

        static int readOperation()
        {
            int result;
            printMenu();
            while (!tryReadInteger(out result) || result < 1 || result > 7)
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
        
        static double incidence7(int day)
        {
            if (day < 6)
            {
                return 0;
            }
            double count = 0;
            for (int i= day-6; i <= day ; i++)
            {
                count = count + cases[i];
            }
            return Math.Round(count/ (10000000 / 100000), 2);
        }

        static double incidence14(int day)
        {
            if (day < 13)
            {
                return 0;
            }
            double count = 0;
            for (int i = day - 13; i <= day; i++)
            {
                count = count + cases[i];
            }
            return Math.Round(count / (10000000 / 100000), 2);
        }

        static double casesPer100K(int firstDay, int lastDay)
        {
            int dayCount = lastDay - firstDay + 1;
            double soucet = 0;
            for (int i = firstDay; i <= lastDay; i++)
            {
                soucet = soucet + cases[i];
            }
            return soucet * 100000 / 10000000;
        }
        static int minDay(int firstDay, int lastDay, out int minimum)
        {
            int day = -1;
            minimum = int.MaxValue;
            for (int j = firstDay; j <= lastDay; j++)
            {
                if (cases[j] < minimum)
                {
                    minimum = cases[j];
                    day = j;
                }
            }
            return day;
        }
        static int maxDay(int firstDay, int lastDay)
        {
            int day = -1;
            int maximum = int.MinValue;
            for (int i = firstDay; i <= lastDay; i++)
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
            for (int i = firstDay; i <= lastDay; i++)
            {
                Console.WriteLine("Den {0} přibylo {1} případů, R = {2}, incidence7 = {3}, incidence14 = {4} ",
                               i + 1, cases[i], simpleR(i), incidence7(i), incidence14(i));
            }
            Console.WriteLine("Zobrazuji záznam z {0} až {1} dne.", firstDay+1, lastDay +1);
            if (lastDay - firstDay + 1 > 0)
            {
                Console.WriteLine("Pokud je R nebo incidence 0, chybí pro výpočet údaje.");
                printStats(firstDay, lastDay);
            }
            return;

        }
        static void printStats(int firstDay, int lastDay)
        {
            Console.WriteLine();
            Console.WriteLine("Průměr pro dny {0} až {1} je: {2}", firstDay + 1, lastDay + 1, casesPerDayAvarage(firstDay, lastDay));
            Console.WriteLine("Počet nakažených na 100 tisíc obyvatel je " + casesPer100K(firstDay, lastDay));
            int a = firstDay;
            if (firstDay == 0)
            {
                a = firstDay + 1;
            }
            Console.WriteLine((minDay(a, lastDay, out int minimum) + 1) + ".den bylo nalezeno minimum: " + minimum);
            int dayx = maxDay(firstDay, lastDay);
            Console.WriteLine(dayx + 1 + ".den" + " bylo nalezeno maximum: " + cases[dayx]);
        }

        static int readDayNumber()
        {
            int result;
            while (!tryReadInteger(out result) || result <1 || result > cases.Length)
            {
                Console.WriteLine();
                Console.WriteLine("Chybné zadání, napište existující den.");
            }
            return result-1;
        }
        static double casesPerDayAvarage(int firstDay, int lastDay)
        {
            int dayCount = lastDay - firstDay + 1;
            double caseCount = 0;
            for (int i = firstDay; i <= lastDay; i++)
            {
                caseCount = caseCount + cases[i];
            }
            return Math.Round (caseCount/dayCount, 2);
        }
    }
} 
    


