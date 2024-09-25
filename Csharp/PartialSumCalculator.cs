namespace Csharp
{
    public class PartialSumCalculator
    {
        private int _startIndex;
        private int _step;
        private SumCalculator _calculator;

        public PartialSumCalculator(int startIndex, int step, SumCalculator calculator)
        {
            _startIndex = startIndex;
            _step = step;
            _calculator = calculator;
        }

        public void CalculatePartialSum()
        {
            for (int i = _startIndex; i < _calculator.CurrentStopIndex; i += _step)
            {
                _calculator.SetNumber(i, _calculator.GetArrayNumber(i) + _calculator.GetArrayNumber((_calculator.ArrayLength - _calculator.CurrentStopIndex) - i));
            }
        }
    }
}