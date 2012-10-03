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
    //Kein Modifizierer bedeutet für Klassen: internal, d.h. nur in der Bibliothek / Anwendung sichtbar
    class Kunde
    {
        //Kein Modifizierer innerhalb einer Klasse bedeutet: private, d.h. nur innerhalb der Klasse (nimmt nicht aktiv am Vererbungsprozess teil) sichtbar

        // Strings werden zwar wie Werttypen behandelt (immutable), sind aber Klassen, d.h. Referenztypen
        string name;
        // Int sind immer 32 bit (4 byte) Ganzzahlen - für 64 bit (8 byte) long verwenden --- kürzer geht zwar auch, hat aber Nachteile
        int alter;
        // Für Geldbeträge sollte man einen Datentyp mit einer fixierteren Anzahl an Dezimalstellen nehmen - das ist decimal
        decimal konto; // genauer gesagt sind hier zwar 16 byte (128 bit) am Start (wie bei quad precision type), aber eben fixiert!

        //Öffentlich Zugreifbarer Konstruktor
        public Kunde(string name)
        {
            //Will man auf Mitglieder einer Klasse zugreifen, so kann man dies mit this. tun
            this.name = name;
            //Setzen von Zahlen und ähnlichen geht analog zu C
            alter = 12;
            //Um zwischen float, double und decimal zu Unterscheiden gibt es die Suffixe f für float, d für double (unnötig da Standardwert) und m für decimal
            konto = 4.5m;
        }

        //Ein Getter wie man ihn in C kennt
        public string GetName()
        {
            return name;
        }

        //Ein Getter über eine sog. Eigenschaft - neu in C#
        public string Name
        {
            get { return name; }
        }

        //Obwohl wir keine Vererbung durchführen erben wir automatisch von Object - und damit die Möglichkeit die mitgelieferten Methoden (z.B. ToString()) zu überschreiben
        public override string ToString()
        {
            //Mit base. greift man auf die Basisklasse zu - ihre öffentlichen und geschützten (aber nicht privaten) Mitglieder
            //return base.ToString(); // würde die Implementierung der Basisklasse aufrufen

            return string.Format("Unser Kunde heißt {0} mit einem Kontostand von {1}.", name, konto);
        }
    }
}
