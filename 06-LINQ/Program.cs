using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

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
            //Bitte die Datei "Sample.xml" beachten
            XDocument xmlDoc = XDocument.Load("Sample.xml");

            //Zunächst mal die SQL ähnliche Syntax
            LinqSqlSyntax(xmlDoc);

            //Nun die mehr verbreitete Extension Method Syntax
            LinqFunctionSyntax(xmlDoc);

            //Zeigen wir mal die verzögerte Ausführung
            LinqDeferedExecution();

            //Suchen wir mal Duplikate
            LinqFindDuplicate(xmlDoc);

            //Verwenden wir mal Mengentheorie
            LinqSetTheory();
        }

        static void LinqSqlSyntax(XDocument doc)
        {
            //Verwenden var um uns die Schreibarbeit von IEnumerable<XElement> sparen zu können (ist aber noch wenig im Ggs. zu den meisten Fällen)
            var enumeration = doc.Descendants("Item");
            //Vorsicht: Select erst am Schluss - und nicht am Anfang (größter Unterschied)
            var simpleQuery = from simpleItem in enumeration
                              where Convert.ToInt32(simpleItem.Element("Number").Value) > 20
                              select simpleItem.Value;

            PrintResults("SQL Query", simpleQuery);
        }

        static void LinqFunctionSyntax(XDocument doc)
        {
            //Verwenden wieder var
            var enumeration = doc.Descendants("Item");
            //Prinzipiell das selbe - nur wollen wir hier andere Werte (> 60, statt > 20)
            var simpleQuery = enumeration
                              .Where(m => Convert.ToInt32(m.Element("Number").Value) > 60)
                              .Select(m => m.Value);

            PrintResults("Function Query", simpleQuery);
        }

        static void LinqDeferedExecution()
        {
            //Implizit eingegebenes Array - hätten auch new int[] { ... } schreiben können
            int[] numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 10 };

            //Erwartet: 1, 4, 9, 16, 25, 36, 49, 100
            var result = numbers.Select(m => m * m);

            numbers[0] = 0; //1 -> 0
            numbers[6] = 9; //7 -> 9

            //Ausgegeben: 0, 4, 9, 16, 25, 36, 81, 100
            PrintResults("Verzögerte Ausführung", result);

            //Queries werden erst zur Ausgabe berechnet - d.h. solange wir keine FIXE Liste erzeugen (z.B. mit ToList(), ToArray(), ...), die einen Zustand
            //fixiert, wird das Query immer bei der Ausgabe berechnet (teuer und u.u. nicht die Ausgabe die man wollte)
        }

        static void LinqFindDuplicate(XDocument doc)
        {
            //Kennen wir schon alles
            var enumeration = doc.Descendants("Item");
            var duplicates = enumeration
                             .GroupBy(x => x.Value) //Gruppieren nach unserem Suchkriterium
                             .Where(g => g.Count() > 1) //Alle die öfter als 1 x vorhanden sind
                             .Select(m => m.Key); //Unseren Schlüssel wählen wir am Ende aus

            //Die Duplikate ausgeben (Duplikate sind in diesem Fall diese, die die selbe Textausgabe (direkte Unterknotenwerte verbunden)
            PrintResults("Duplikate", duplicates);
        }

        static void LinqSetTheory()
        {
            var half = "abcdefghijklmn".ToCharArray();
            var odd = "bdfhjlnprtvxz".ToCharArray();

            var union = half.Union(odd).ToArray();
            var intersection = half.Intersect(odd).ToArray();
            var except = half.Except(odd).ToArray();
            var symmetricExcept = half.SymmetricExcept(odd).ToArray();

            //Alles (aber keine (!) Duplikate), d.h. erste Hälfte des Alphabets komplett und 2. Hälfte nur jedes 2.
            PrintResults("Vereinigungsmenge", union);
            //Nur was in half UND odd vorkommt, d.h. nur jedes 2. Element der ersten Hälfte des Alphabets (ab dem 2.)
            PrintResults("Schnittmenge", intersection);
            //Nur was NUR in half vorkommt, d.h. nur jedes 2. Element der ersten Hälfte des Alphabets (ab dem 1.)
            PrintResults("Differenzmenge", except);
            //Alles was in half und ODD vorkommt, ohne dass was in beiden vorkommt, d.h. jedes 2. Element der ersten Hälfte (ab dem 1.) und jedes 2. Element (ab 2.) der 2. Hälfte des Alphabets
            PrintResults("Symmetrische Differenz", symmetricExcept);
        }

        static void PrintResults<T>(string name, IEnumerable<T> elements)
        {
            Console.WriteLine("------------");
            Console.WriteLine(name);
            Console.WriteLine("------------");

            foreach (var element in elements)
            {
                Console.WriteLine(element);
            }
        }
    }

    //Um uns Schreibarbeit zu sparen (erst nützlich bei mehrmaligen (min. 2) Verwendung) legen wir eine Erweiterungsmethode an
    static class LinqSymmetric
    {
        //Verwenden Generics (im LINQ Stil)
        public static IEnumerable<T> SymmetricExcept<T>(this IEnumerable<T> left, IEnumerable<T> right)
        {
            //Nutzen nur bereits vorhandene LINQ Methoden
            return left.Union(right).Except(left.Intersect(right));
        }
    }
}
