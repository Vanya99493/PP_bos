using System.Threading;

namespace Csharp
{
    public class SumCalculator
    {
        private long[] _array;
        private int _completedPartialSums;

        private Semaphore _calculationSemaphore = new Semaphore(0, 1);

        public int ArrayLength => _array.Length;
        public int CurrentStopIndex { get; private set; }

        //
        // + testing on properly work
        //

        public long CalculateSum(long[] array, int threadsCount)
        {
            _array = array;
            CurrentStopIndex = array.Length;
            long sum = 0;

            Thread[] threads = new Thread[threadsCount];
            // var partialCalculators = initialize calculators
            // threads = InitializeThreads()

            while(CurrentStopIndex >= 1)
            {
				CurrentStopIndex = CurrentStopIndex / 2 + CurrentStopIndex % 2;
                for (int i = 0; i < CurrentStopIndex && i < threads.Length; i++)
                {
                    //threads[i] = new Thread(new ThreadStart(new PartialSumCalculator(i, threadsCount, this).CalculatePartialSum));
                    // =>
                    // partial calc .Initialize()
                    // thread[i].Start()
                    threads[i].Start();
                }

				_calculationSemaphore.WaitOne();
            }

            return sum;
        }

        public void SetNumber(int arrayIndex, long partialSum)
        {
            _array[arrayIndex] = partialSum;
            if(_completedPartialSums >= CurrentStopIndex)
            {
                // *******
                _calculationSemaphore.Release();
            }
        }

        public long GetArrayNumber(int index) => _array[index];
    }
}