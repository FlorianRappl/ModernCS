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
        const int ITERATIONS = 300000000;

        static void Main(string[] args)
        {
            Benchmark(() => SerialCode());
            Benchmark(() => ParallelCode());
        }

        private static void Benchmark(Func<double> method)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            var pi = method();
            timer.Stop();
            Console.WriteLine("Benötigte Zeit: {0}ms", timer.ElapsedMilliseconds);
            var precision = Math.Abs(Math.PI - pi);
            Console.WriteLine("Pi ({1}) konnte mit einer Genauigkeit von {0} ermittelt werden.", precision, pi);
        }

        static double SerialCode()
        {
            var sum = 0.0;
            var step = 1.0 / (double)ITERATIONS; 

            for(var i = 0; i < ITERATIONS; i++)
            {
                double x = (i + 0.5) * step;
                sum = sum + 4.0 / (1.0 + x * x); 
            }

            return sum * step;
        }

        static double ParallelCode()
        {
            var sum = 0.0;
            var step = 1.0 / (double)ITERATIONS;
            //Brauchen wir nur zum Locken - da müssen wir eine Speicheradresse / Referenz angeben
            var monitor = new object();

            Parallel.For(0, ITERATIONS, () => 0.0, (i, state, local) =>
            {
                double x = (i + 0.5) * step;
                return local + 4.0 / (1.0 + x * x);
            }, local => 
            {
                lock (monitor) 
                    sum += local;
            });

            return sum * step;
        }
    }
}
