using System;
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
    public class Kunde : IMoney
    {
        //Unsere globalen Variablen
        Dictionary<string, decimal> accounts;

        //Der Konstruktor
        public Kunde()
        {
            Id = Guid.NewGuid();
            accounts = new Dictionary<string, decimal>();
        }

        //Regions sind sehr nützlich um den Editor übersichtlich zu halten
        #region Eigenschaften

        public Guid Id { get; private set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Birthday { get; set; }

        #endregion

        #region Methoden (beide von IMoney)

        public void AddMoney(decimal amount, string account)
        {
            if (accounts.ContainsKey(account))
                accounts[account] += amount;
            else
                accounts.Add(account, amount);
        }

        public decimal GetMoney(string account)
        {
            if (accounts.ContainsKey(account))
                return accounts[account];

            return 0m;
        }

        #endregion
    }
}
