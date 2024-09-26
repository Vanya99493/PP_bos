using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Csharp
{
    public class SumCalculator
    {
        private long[] _array;
        private int[] _completedPartialSumsArray;
        private Semaphore _calculationSemaphore;
        private bool _isSemaphoreWaiting;
        private CancellationTokenSource _threadManagerCanceletionTokenSource;

        public int ArrayLength => _array.Length;
        public int CurrentStopIndex { get; private set; }
        public int PreviousStopIndex { get; private set; }

        public long GetArrayNumber(int index) => _array[index];

        public long CalculateSum(long[] array, int threadsCount, out float calculationTime)
        {
            _array = array;
            CurrentStopIndex = array.Length;
            _calculationSemaphore = new Semaphore(0, 1);

            InitializeAndStartThreads(threadsCount, out var threads, out var partialSumCalculators);
            InitializeAndStartThreadManager();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            CalculateSum(threads, partialSumCalculators);
            stopwatch.Stop();

            calculationTime = (float)stopwatch.Elapsed.TotalSeconds;
            long sum = _array[0];

            StopThreads(partialSumCalculators);
            DeinitializeArrays();

            return sum;
        }

        public void SetNumber(int arrayIndex, long partialSum, int _calculatorId)
        {
            _array[arrayIndex] = partialSum;
            _completedPartialSumsArray[_calculatorId]++;
        }

        private void InitializeAndStartThreads(int threadsCount, out Thread[] threads, out PartialSumCalculator[] partialSumCalculators)
        {
            threads = new Thread[threadsCount];
            partialSumCalculators = new PartialSumCalculator[threadsCount];
            _completedPartialSumsArray = new int[threadsCount];

            for (int i = 0; i < threads.Length; i++)
            {
                partialSumCalculators[i] = new PartialSumCalculator(i);
                threads[i] = new Thread(new ThreadStart(partialSumCalculators[i].CalculatePartialSum));
                threads[i].Start();
            }
        }

        private void InitializeAndStartThreadManager()
        {
            _threadManagerCanceletionTokenSource = new CancellationTokenSource();
            Thread threadsManager = new Thread(new ThreadStart(CheckIfCompletePartialCalculators));
            threadsManager.Start();
        }

        private void CalculateSum(Thread[] threads, PartialSumCalculator[] partialSumCalculators)
        {
            while (CurrentStopIndex > 1)
            {
                for (int i = 0; i < _completedPartialSumsArray.Length; i++)
                {
                    _completedPartialSumsArray[i] = 0;
                }
                PreviousStopIndex = CurrentStopIndex;
                CurrentStopIndex = CurrentStopIndex / 2 + CurrentStopIndex % 2;
                for (int i = 0; i < CurrentStopIndex && i < threads.Length; i++)
                {
                    partialSumCalculators[i].Initialize(i, threads.Length, this, true);
                }

                if (_completedPartialSumsArray.Sum() < CurrentStopIndex)
                {
                    _isSemaphoreWaiting = true;
                    _calculationSemaphore.WaitOne();
                }
            }
        }

        private void StopThreads(PartialSumCalculator[] partialSumCalculators)
        {
            _threadManagerCanceletionTokenSource.Cancel();
            for (int i = 0; i < partialSumCalculators.Length; i++)
            {
                partialSumCalculators[i].Deinitialize();
            }
        }

        private void DeinitializeArrays()
        {
            _array = null;
            _completedPartialSumsArray = null;
        }

        private void CheckIfCompletePartialCalculators()
        {
            while (!_threadManagerCanceletionTokenSource.Token.IsCancellationRequested)
            {
                if(_completedPartialSumsArray.Sum() >= CurrentStopIndex)
                {
                    if (_isSemaphoreWaiting)
                    {
                        _isSemaphoreWaiting = false;
                        _calculationSemaphore.Release();
                    }
                }
            }
        }
    }
}