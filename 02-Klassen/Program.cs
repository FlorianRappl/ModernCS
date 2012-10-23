using System;

/*
 * 
 * (c) Florian Rappl, 2012.
 * 
 * This work is a demonstration for training purposes and may be used freely for private purposes.
 * Usage for business training / workshops is prohibited without explicit permission from the author.
 * 
 */

namespace ModernCS
{
    class Program
    {
        static void Main(string[] args)
        {
            //Analog zu C++ erstellt man in C# neue Objekte über new () --> wobei hier Speicher alloziiert wird und sofort der Konstruktor aufgerufen wird
            //Es gibt immer mind. einen Konstruktor - wenn wir keinen angeben, wird automatisch ein leerer Standardkonstruktor erstellt
            Kunde neuerKunde = new Kunde("Hans Mustermann");
            
            Console.Write("Der Name des Kunden: ");
            //Verwenden eines Getters im C++ Stils - wie eine Funktion
            Console.WriteLine(neuerKunde.GetName());

            Console.Write("Der Name des Kunden: ");
            //Verwenden eines Getters im C# Stil (Eigenschaft) - wie eine Variable
            Console.WriteLine(neuerKunde.Name);

            //Was passiert wenn wir WriteLine() direkt das Objekt geben, d.h. keinen String?
            Console.WriteLine(neuerKunde); //WriteLine() ist überladen mit einer Übergabe von Object - dabei wird einfach die ToString() Methode aufgerufen => Nutzen OOP

            BadRecycling();
            Console.WriteLine("Jetzt wurde die Methode BadRecycling verlassen...");

            GoodRecycling();
            Console.WriteLine("Jetzt wurde die Methode GoodRecycling verlassen...");

            Console.WriteLine("Bis jetzt wurde FalseRecycling immer noch nicht gelöscht!");
        }

        //Muss statisch sein, damit Main direkt auf die Methode zugreifen kann
        static void BadRecycling()
        {
            FalseRecycling recycle = new FalseRecycling();
        }

        static void GoodRecycling()
        {
            //Using ist nur möglich bei Objekten die IDisposable implementieren!
            using (RightRecycling recycle = new RightRecycling())
            {
            }
        }
    }
}
