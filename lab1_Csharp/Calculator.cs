using System.Diagnostics;
using System.Threading;

namespace lab1_Csharp
{
    public class Calculator
    {
        public long SumInMultipleThreads(int[] array, int threadsCount, out float calculationTime)
        {
            long sum = 0;

            Thread[] threads = new Thread[threadsCount];
            PartialCalculator[] partialCalculators = new PartialCalculator[threadsCount];

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < threads.Length; i++)
            {
                partialCalculators[i] = new PartialCalculator(array, i, threadsCount);
                threads[i] = new Thread(new ThreadStart(partialCalculators[i].Calculate));
                threads[i].Start();
            }

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
                sum += partialCalculators[i].Sum;
            }

            stopwatch.Stop();
            calculationTime = (float)stopwatch.Elapsed.TotalMilliseconds;

            return sum;
        }
    }
}