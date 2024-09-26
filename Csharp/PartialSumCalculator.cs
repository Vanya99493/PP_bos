using System.Threading;

namespace Csharp
{
    public class PartialSumCalculator
    {
        private int _calculatorId;
        private int _startIndex;
        private int _step;
        private SumCalculator _calculator;
        private bool _isWorking = false;
        private Semaphore _isWorkingSemaphore = new Semaphore(0, 1);

        public PartialSumCalculator(int id)
        {
            _calculatorId = id;
        }

        public void Initialize(int startIndex, int step, SumCalculator calculator, bool isWorking = false)
        {
            _startIndex = startIndex;
            _step = step;
            _calculator = calculator;
            _isWorking = isWorking;
            if (_isWorking)
            {
                _isWorkingSemaphore.Release();
            }
        }

        public void CalculatePartialSum()
        {
            while (true)
            {
                _isWorkingSemaphore.WaitOne();

                if (!_isWorking)
                {
                    break;
                }

                for (int i = _startIndex; i < _calculator.CurrentStopIndex; i += _step)
                {
                    int secondNumberIndex = _calculator.ArrayLength - (_calculator.ArrayLength - _calculator.PreviousStopIndex) - i - 1;

                    _calculator.SetNumber(
                        i,
                        i == secondNumberIndex 
                            ? _calculator.GetArrayNumber(i) 
                            : _calculator.GetArrayNumber(i) + _calculator.GetArrayNumber(secondNumberIndex),
                        _calculatorId
                        );
                }
            }
        }

        public void Deinitialize()
        {
            _isWorking = false;
            _isWorkingSemaphore.Release();
        }
    }
}