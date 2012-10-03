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
            Console.Write("Bitte gib deinen Namen ein: ");
            string name = Console.ReadLine();
            Console.WriteLine("Hallo, " + name + "!");
            Console.WriteLine("Das Ergebnis von 7 * 3 ist " + (7 * 3) + ".");
            Console.WriteLine("Noch ein Tip für dich {0}: In C# sind Formatierer anders aufgebaut als z.B. in C.", name);
        }
    }
}
