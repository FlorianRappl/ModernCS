using System;
using System.Collections;
using System.Collections.Generic;

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
            //Super nützliche Klasse z.B. für Variable Objekte
            Hashtable table = new Hashtable();
            //Signatur hier ist: Object, Object
            table.Add("Ein Key", "Sein Wert");
            //Somit sind wir weder beim Key, noch beim Wert type-safe ...
            table.Add("Zweiter Key", 17);

            int summe = 0;

            //Wir verwenden einfach mal den Overhead eines try-catchs um (leider sinnvollerweise hier) die Casts abzusichern
            try
            {
                //D.h. neben Casts (teuer), brauchen wir try-catch (sehr teuer)

                //Wir müssen daher Casts anwenden
                foreach (object key in table.Keys)
                    summe += (int)table[key];//Wirft keinen Compiler-Fehler! -- aber Vorsicht im Programm...

            }
            catch (Exception ex)
            {
                Console.WriteLine("War auch irgendwie zu erwarten - oder? Der genaue Fehler hier ist:");
                Console.WriteLine(ex.Message);
            }

            //Viel besser gehts mit Generics
            Dictionary<string, int> dict = new Dictionary<string, int>();
            //Signatur hier ist nun: string, int
            dict.Add("Ein Key", 7);
            //Geht gar nichts mehr anderes - sonst gibts gleich Compiler-Fehler
            dict.Add("Zweiter Key", 17);

            int sum = 0;

            foreach (string key in dict.Keys)
                sum += dict[key];//Kein Cast mehr notwendig - und Typensicher! - da geht nichts mehr schief

            Console.WriteLine("Die Summe ist: {0}", sum);
        }
    }
}
