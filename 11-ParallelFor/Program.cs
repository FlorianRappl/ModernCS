using System;
using System.Threading.Tasks;
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
        const int ITERATIONS = 5000000;

        static void Main(string[] args)
        {
            Benchmark(() => SerialCode());
            Benchmark(() => ParallelCode());
        }

        private static void Benchmark(Func<decimal> method)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            var pi = method();
            timer.Stop();
            Console.WriteLine("Benötigte Zeit: {0}ms", timer.ElapsedMilliseconds);
            var precision = Math.Abs((decimal)Math.PI - pi);
            Console.WriteLine("Pi ({1}) konnte mit einer Genauigkeit von {0} ermittelt werden.", precision, pi);
        }

        static decimal SerialCode()
        {
            var sum = 0m;
            var step = 1m / (decimal)ITERATIONS; 

            for(var i = 0; i < ITERATIONS; i++)
            {
                decimal x = (i + 0.5m) * step;
                sum = sum + 4m / (1m + x * x); 
            }

            return sum * step;
        }

        static decimal ParallelCode()
        {
            var sum = 0m;
            var step = 1m / (decimal)ITERATIONS;
            //Brauchen wir nur zum Locken - da müssen wir eine Speicheradresse / Referenz angeben
            var monitor = new Object();

            Parallel.For(0, ITERATIONS, () => 0m, (i, state, local) =>
            {
                decimal x = (i + 0.5m) * step;
                return local + 4m / (1m + x * x);
            }, local => 
            {
                lock (monitor)
                {
                    sum += local;
                }
            });

            return sum * step;
        }
    }
}
