import java.util.concurrent.Semaphore;

public class ThreadManager extends Thread
{
    public Boolean IsSemaphoreWaiting;
    public Boolean IsCanceled;

    private SumCalculator _calculator;
    private Semaphore _calculationSemaphore;

    public ThreadManager(SumCalculator calculator)
    {
        _calculator = calculator;
        _calculationSemaphore = new Semaphore(0);
        IsCanceled = false;
    }

    public void acquireCalculationSemaphore()
    {
        try {
            _calculationSemaphore.acquire();
        } catch (InterruptedException e) {
            throw new RuntimeException(e);
        }
    }

    @Override
    public void run()
    {
        while(!IsCanceled)
        {
            if (_calculator.getCompletedPartialSums() >= _calculator.getCurrentStopIndex())
            {
                if(IsSemaphoreWaiting)
                {
                    IsSemaphoreWaiting = false;
                    _calculationSemaphore.release();
                }
            }
        }
    }
}
