namespace Csharp
{
    public class SumCalculator
    {
        private int[] _array;
        private long[] _sumArray;
        private int _currentStep;

        public int CurrentStep => _currentStep;
        public int ArrayLength => _array.Length;
        public int CurrentStopIndex { get; private set; }

        public SumCalculator()
        {

        }

        public long CalculateSum(int[] array, int threadsCount)
        {
            _array = array;
            long sum = 0;



            return sum;
        }

        public void AddPartialSum(int threadIndex, long partialSum)
        {
            _sumArray[threadIndex] = partialSum;
        }

        public int GetArrayNumber(int index) => _array[index];
    }
}