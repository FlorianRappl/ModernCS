using System;
using System.Text;
using System.Dynamic;
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
    //Man kann allerhand dynamischer Objekte erzeugen - müssen nur von DynamicObject erben (liegt in DLR)
    public class DynKunde : DynamicObject
    {
        //Hier wird im Hintergrund mit einem Dictionary gearbeitet
        Dictionary<string, object> properties = new Dictionary<string, object>();

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            //Nuten die Methode TryGetValue von Dictionary<K,V> aus
            return properties.TryGetValue(binder.Name, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            //Nutzen binder.Name aus
            properties[binder.Name] = value;
            return true;
        }

        public Dictionary<string, object> GetProperties()
        {
            return properties;
        }

        public override string ToString()
        {
            //Zur Verkettung vieler Strings (mehr als 3) immer den StringBuilder verwenden
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("---Kundendaten---");

            foreach (var key in properties.Keys)
            {
                sb.Append(key).Append(": ").AppendLine(properties[key].ToString());
            }

            return sb.ToString();
        }
    }
}
