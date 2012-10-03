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
            //Jeder statische Typ kann als dynamic angenommen ("gecasted") werden - dies wird implizit gemacht bei Kommunikation
            //mit dynamischen (Skript-)sprachen wie z.B. Python, PHP, JavaScript ...
            dynamic ran = new Random();

            //Kein Intelli-Sense mehr!
            var i = ran.Next(0, 100); //Der Typ hier ist dynamic (mal auf var schauen)

            //Ausgabe an Console.WriteLine funktioniert immer noch
            Console.WriteLine(i);

            //Cooler Trick - wir können nun Rechnungen vornehmen, ohne dass der Compiler weiß ob es gehen wird
            var bla = i + 3; //Der Typ hier ist dynamic (3 wird auf dynamic gecasted)

            //Analog
            Console.WriteLine(bla);

            //Wichtig hier ist, dass wir die Instanz als "dynamic" behandeln
            dynamic mykunde = new DynKunde();
            mykunde.FirstName = "Florian";
            mykunde.LastName = "Rappl";
            mykunde.Age = 28;
            mykunde.Joined = new DateTime(1996, 10, 5);
            mykunde.Internet = "www.florian-rappl.de";
            mykunde.EMail = "mail@florian-rappl.de";

            //Hier sehen wir auch: Die Ausgabe bei Console.WriteLine wird über Object --> ToString() erzeugt.
            Console.WriteLine(mykunde);
        }
    }
}
