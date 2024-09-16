public class Calculator
{
    private long _sum;
    private int _threadsCount;
    private int _endedThreads;

    public long sumInMultipleThreads(int[] array, int threadsCount)
    {
        _sum = 0;
        _threadsCount = threadsCount;
        _endedThreads = 0;

        PartialCalculator[] partialCalculators = new PartialCalculator[threadsCount];

        for (int i = 0; i < threadsCount; i++)
        {
            partialCalculators[i] = new PartialCalculator(this, array, i, threadsCount);
            partialCalculators[i].start();
        }

        waitForEndSumCalculation();

        return _sum;
    }

    synchronized public void addSum(long value)
    {
        _sum += value;
        _endedThreads++;
        notify();
    }

    synchronized public void waitForEndSumCalculation()
    {
        while(_endedThreads < _threadsCount)
        {
            try
            {
                wait();
            }
            catch (InterruptedException e)
            {
                e.printStackTrace();
            }
        }
    }
}