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
    class FalseRecycling
    {
        int someVariable;

        public FalseRecycling()
        {
            someVariable = 5;
        }

        //Nun ein besseres (mit Setter!) Beispiel zu Eigenschaften
        public int SomeVariable 
        {
            get { return someVariable; }
            set
            {
                //Zugriff auf die Übergabe über "value"
                someVariable = value;
                //Etwas im Debug-Fenster des VS ausgeben --> wird ignoriert bei nicht-VS Hostprozessanwendungen
                Debug.WriteLine("Zugriff auf den Setzer der Eigenschaft...");
            }
        }

        //Destruktor a la C++ geht - Modifizierer sind nicht notwendig, da WIR ihn sowieso NICHT aufrufen können
        //-> heißt ja Managed Code, da der GC diese Aufgabe übernimmt - nur wann?!
        ~FalseRecycling()
        {
            Console.WriteLine("FalseRecycling wurde gelöscht - d.h. der Destruktor wurde wohl aufgerufen...");
        }
    }

    //Vererbung über den ":" - wie in C++ - hier implementieren wir Interface (sowas wie eine "pure" abstrakte Klasse)
    class RightRecycling : IDisposable //wir können nur max. von 1 Klasse erben, dafür aber bel. viele Interfaces einbauen
    {
        int someVariable;

        public RightRecycling()
        {
            someVariable = 5;
        }

        //Die Dispose() Methode sollte alle noch zu schließenden Resourcen / Handles wieder freigeben
        public void Dispose()
        {
            Console.WriteLine("Resourcen von RightRecycling wurden freigegeben - da die Dispose() Methode aufgerufen wurde...");
        }
    }
}
