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
            int a = 5;            
            Console.WriteLine("a ist = {0}", a);
            //Kann weiterhin Operationen die nur für den Typ wie z.B. Multiplikation (für Integer) ausführen - ginge nicht bei allgemeiner Übergabe von z.B. "object"
            a = Identity(a) * Identity(17);
            Console.WriteLine("a ist nun = {0}", a);

            int b = 9;
            int c = 17;

            Console.WriteLine("Ausgangswerte: b = {0}, c= {1}", b, c);

            //b und c sind Wert-Typen => werden ohne "ref" nur als Kopien übergeben
            Swap(ref b, ref c);

            Console.WriteLine("Nach Vertauschen: b = {0}, c= {1}", b, c);

            //Hier nochmal vertauschen FALLS b > c (ist wohl gegeben)
            SwapIfGreater(ref b, ref c);

            Console.WriteLine("Vertauschen falls b > c: b = {0}, c= {1}", b, c);

            //Nochmal das selbe (hat auf jeden Fall keine Auswirkungen, ist eine sog. idempotente Operation)
            SwapIfGreater(ref b, ref c);

            Console.WriteLine("Nochmals Vertauschen falls b > c: b = {0}, c= {1}", b, c);
        }

        //Sehr einfaches Beispiel - ermitteln den Typ aus dem ersten Parameter - und nutzen diesen Typ zur Rückgabe
        static T Identity<T>(T parameter)
        {
            return parameter;
        }

        //Generische Swap Methode zum Vertauschen von Variablen (egal ob struct oder class, d.h. Wert- oder Referenztyp)
        static void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp;
            temp = lhs;
            lhs = rhs;
            rhs = temp;
        }

        //Wie bei Klassen können wir auch bei Methoden sog. Constraints (Einschränkungen) verwenden um Grundfunktionalität der Parameter festzulegen
        static void SwapIfGreater<T>(ref T lhs, ref T rhs) where T : IComparable<T>
        {
            T temp;

            //CompareTo existiert, da wir den Constraint eingeführt haben -- kommt von IComparable<T>
            if (lhs.CompareTo(rhs) > 0)
            {
                temp = lhs;
                lhs = rhs;
                rhs = temp;
            }
        }
    }
}
