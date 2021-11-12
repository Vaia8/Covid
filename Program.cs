using System;

namespace Covid
{
    class Program
    {
        static int[] cases = new int[] { 0, 4, 15, 30, 45, 60, 82, 110, 150, 193 };

        static void Main(string[] args)
        {
            while (true)
            {
                int operation = readOperation();

                switch (operation)
                {
                    case 1:
                        Console.WriteLine("Máme záznam z " + cases.Length + " dnů");
                        Console.WriteLine();
                        for (int i = 0; i < cases.Length; i = i + 1)
                        {
                            Console.WriteLine("Den " + i + " přibylo " + cases[i] + " případů");
                        }
                        break;
                    case 2:
                        break;
                    case 3:
                        Array.Resize(ref cases, cases.Length + 1);
                        cases[cases.Length - 1] = readInteger();
                        break;
                    case 4:
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
            Console.WriteLine("Zadej číslo: ");
            while (!tryReadInteger(out result)) 
             {
                Console.WriteLine("Chybné zadání, napište platné číslo operace: ");
             }
            return result;
            
        }
    }
}
