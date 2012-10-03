using System;
using System.Reflection;
using System.Diagnostics;


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
            Stopwatch watch = new Stopwatch();
            watch.Start();

            //Geben Reflection ein von uns erstelles Objekt
            Inspect(new Kunde());

            //Geben Reflection ein .NET-Framework Object
            Inspect(watch);

            //Geben Reflection ein anonymes Objekt
            Inspect(new {
                Name = "Florian",
                Age = 28,
                Whatever = 5.0,
                Birthday = new DateTime(1984,1,1)
            });

            watch.Stop();

            Console.WriteLine("Die vergangene Zeit ist {0}ms!", watch.ElapsedMilliseconds);
        }

        static void Inspect(object data)
        {
            Console.WriteLine("------------");
            Console.WriteLine("Starte Untersuchung ...");
            Console.WriteLine("------------");

            //Reflection auf den Typ
            Type info = data.GetType();

            Console.WriteLine("Der Name des Typs ist {0}, der vollständiger Name ist {1}.", info.Name, info.FullName);

            //Auslesen aller Eigenschaften
            PropertyInfo[] properties = info.GetProperties();

            foreach (var prop in properties)
            {
                Console.WriteLine("Eigenschaft {0} mit Typ {1} gefunden (lesen: {2}, schreiben: {3})...", prop.Name, prop.PropertyType.Name, prop.CanRead, prop.CanWrite);
            }

            MethodInfo[] methods = info.GetMethods();

            foreach (var meth in methods)
            {
                Console.WriteLine("Methode {0} mit Rückgabetyp {1} gefunden ({2} Parameter)...", meth.Name, meth.ReturnType.Name, meth.GetParameters().Length);
            }
        }
    }

    class Kunde
    {
        //Getter und Setter mit sog. Autovervollständigung (Compiler legt die Variablen dazu fest)
        public string Name { get; set; }

        //Man beachte das private setzen des Setters - ansonsten wären diese auto-properties ziemlich unnötig / schlecht
        public int Age { get; private set; }

        public bool Rabatt { get; set; }
    }
}
