import java.util.concurrent.Semaphore;

public class PartialSumCalculator extends Thread
{
    private int _calculatorId;
    private int _startIndex;
    private int _step;
    private SumCalculator _calculator;
    private Boolean _isWorking = false;
    private Semaphore _calculationSemaphore = new Semaphore(0);

    public PartialSumCalculator(int id)
    {
        _calculatorId = id;
    }

    public void initialize(int startIndex, int step, SumCalculator calculator, Boolean isWorking)
    {
        _startIndex = startIndex;
        _step = step;
        _calculator = calculator;
        _isWorking = isWorking;
        if (_isWorking)
        {
            _calculationSemaphore.release();
        }
    }

    @Override
    public void run()
    {
        while (true)
        {
            try {
                _calculationSemaphore.acquire();
            } catch (InterruptedException e) {
                throw new RuntimeException(e);
            }

            if (!_isWorking)
            {
                break;
            }

            for (int i = _startIndex; i < _calculator.getCurrentStopIndex(); i += _step)
            {
                int secondNumberIndex = _calculator.getArrayLength() - (_calculator.getArrayLength() - _calculator.getPreviousStopIndex()) - i - 1;

                _calculator.setNumber(
                        i,
                        i == secondNumberIndex
                                ? _calculator.getArrayNumber(i)
                                : _calculator.getArrayNumber(i) + _calculator.getArrayNumber(secondNumberIndex),
                        _calculatorId
                );
            }
        }
    }

    public void deinitialize()
    {
        _isWorking = false;
        _calculationSemaphore.release();
    }
}
