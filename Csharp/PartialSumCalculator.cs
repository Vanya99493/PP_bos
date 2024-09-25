namespace Csharp
{
    public class PartialSumCalculator
    {
        private int _threadId;
        private int _startIndex;
        private SumCalculator _calculator;

        public PartialSumCalculator(int threadId, int startIndex, SumCalculator calculator)
        {
            _threadId = threadId;
            _startIndex = startIndex;
            _calculator = calculator;
        }

        public void CalculatePartialSum()
        {
            long partialSum = 0;

            for (int i = _startIndex; i < _calculator.ArrayLength; i+= _calculator.CurrentStep)
            {
                partialSum += 
            }

            _calculator.AddPartialSum(_threadId, partialSum);
        }
    }
}