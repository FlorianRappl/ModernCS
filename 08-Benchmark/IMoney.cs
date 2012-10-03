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
    interface IMoney
    {
        void AddMoney(decimal amount, string account);

        decimal GetMoney(string account);
    }
}
