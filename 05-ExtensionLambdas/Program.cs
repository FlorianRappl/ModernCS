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
            //Erstellen eine Variable mit einer Lambda Expression
            Func<string, string> lambda = str =>
            {
                Console.WriteLine("Die Lambda Expression wurde ausgeführt...");
                return (str + str + str + str).Replace("er", string.Empty);
            };

            string mystring = "Bert";

            //Müssen bei Methoden Generics normalerweise den Typen nicht angeben - wird automatisch vom Compiler ermittelt
            mystring.Append(5).Append("cool").Append('c');

            Console.WriteLine("Mystring ist (immer noch): {0}", mystring);

            //Hier weisen wir speichern wir nun die Modifikationen an mystring
            mystring = mystring.Modify(lambda);

            Console.WriteLine("Mystring ist jetzt: {0}", mystring);
        }
    }

    //Extension Methods müssen immer in einer statischen Klasse (wenn außerhalb verwendbar: public Modifizierer) erstellt werden
    static class Extensions
    {
        //Extension Methode auf einen string - daher "this" vor "string"
        public static string Append<T>(this string basis, T what)
        {
            Console.WriteLine("An den String {0} wurde {1} vom Typ {2} angehängt", basis, what, what.GetType().Name);//Mit GetType() beschäftigen wir uns später
            return basis + what.ToString();
        }

        //Extension Methode auf einen string (schon wieder...) - daher "this" und diesmal übergeben wir zusätzlich eine Methode!
        public static string Modify(this string basis, Func<string, string> expression)
        {
            //Führen die mitgegebene Methode aus
            return expression(basis);
        }
    }
}
