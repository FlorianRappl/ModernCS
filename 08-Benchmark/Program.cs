using System;
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
        //Konstanten mit "const" setzen
        const int MAX_ITERATIONS = 1000000;

        static void Main(string[] args)
        {
            Kunde instanz = new Kunde();
            Benchmark(Interface, instanz);
            Benchmark(Reflection, instanz, "Reflection");

            //Nur zur Kontrolle - auf beiden Konten sollte der selbe Betrag sein
            Console.WriteLine("Geld auf dem Konto Interface am Ende: {0}", instanz.GetMoney("Interface"));
            Console.WriteLine("Geld auf dem Konto Reflection am Ende: {0}", instanz.GetMoney("Reflection"));
        }

        //Hier machen wir alles über Reflection - sehr viel Handarbeit notwendig
        static void Reflection(object parameter)
        {
            var typ = parameter.GetType();
            var methods = typ.GetMethods();

            foreach (var method in methods)
            {
                //z.B. das hier (Ok, hätte man normal so nicht; aber man müsste z.B. die Signatur überprüfen)
                if (method.Name.Equals("AddMoney"))
                {
                    //Ausführen der Methode - auch hier ist die Übergabe der Parameter NICHT type-safe
                    method.Invoke(parameter, new object[] { 1.0m, "Reflection" });
                    break;
                }
            }
        }

        //Hier wird alles sehr bequem gehandelt - und alles ist Typensicher
        static void Interface(object parameter)
        {
            //Zunächst mal überprüfen
            if (parameter is IMoney)
            {
                //as wirft keine Exception wenn der Cast nicht klappt - hier eigentlich unnötig
                IMoney typ = parameter as IMoney;
                typ.AddMoney(1.0m, "Interface");
            }
        }

        //Methode zum Durchführen der Benchmarks mit optionalen Namens-Parameter
        static void Benchmark(Action<object> method, object parameter, string name = null)
        {
            //Standardwert für den Namen vergeben
            if(string.IsNullOrEmpty(name))
                name = method.Method.Name;

            int i = 0;
            Stopwatch clock = new Stopwatch();
            clock.Start();

            while (i++ < MAX_ITERATIONS)
            {
                method(parameter);
            }

            clock.Stop();
            Console.WriteLine("Ergebnis für {0}: {1}ms", name, clock.ElapsedMilliseconds);
        }
    }
}
