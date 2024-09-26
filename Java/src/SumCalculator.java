public class SumCalculator
{
    private long[] _array;
    private int[] _completedPartialSumsArray;
    private int _currentStopIndex;
    private int _previousStopIndex;
    private ThreadManager _threadsManager;

    public int getArrayLength()  { return _array.length; }
    public int getCurrentStopIndex() { return _currentStopIndex; }
    public int getPreviousStopIndex() { return _previousStopIndex; }
    public long getArrayNumber(int index) { return _array[index]; }

    public int getCompletedPartialSums()
    {
        int completedPartialSums = 0;
        for (int i = 0; i < _completedPartialSumsArray.length; i++)
        {
            completedPartialSums += _completedPartialSumsArray[i];
        }
        return completedPartialSums;
    }

    public long calculateSum(long[] array, int threadsCount)
    {
        _array = array;
        _currentStopIndex = array.length;

        PartialSumCalculator[] partialSumCalculators = initializeAndStartThreads(threadsCount);
        initializeAndStartThreadManager();

        calculateSum(partialSumCalculators);
        long sum = array[0];

        stopThreads(partialSumCalculators);
        deinitializeArrays();

        return sum;
    }

    public void setNumber(int arrayIndex, long partialSum, int calculatorId)
    {
        _array[arrayIndex] = partialSum;
        _completedPartialSumsArray[calculatorId]++;
    }

    private PartialSumCalculator[] initializeAndStartThreads(int threadsCount)
    {
        PartialSumCalculator[] partialSumCalculators = new PartialSumCalculator[threadsCount];
        _completedPartialSumsArray = new int[threadsCount];

        for (int i = 0; i < threadsCount; i++)
        {
            partialSumCalculators[i] = new PartialSumCalculator(i);
            partialSumCalculators[i].start();
        }

        return partialSumCalculators;
    }

    private void initializeAndStartThreadManager()
    {
        _threadsManager = new ThreadManager(this);
        _threadsManager.start();
    }

    private void calculateSum(PartialSumCalculator[] partialSumCalculators)
    {
        while (_currentStopIndex > 1)
        {
            for (int i = 0; i < _completedPartialSumsArray.length; i++)
            {
                _completedPartialSumsArray[i] = 0;
            }
            _previousStopIndex = _currentStopIndex;
            _currentStopIndex = _currentStopIndex / 2 + _currentStopIndex % 2;
            for (int i = 0; i < _currentStopIndex && i < partialSumCalculators.length; i++)
            {
                partialSumCalculators[i].initialize(i, partialSumCalculators.length, this, true);
            }

            if (getCompletedPartialSums() < _currentStopIndex)
            {
                _threadsManager.IsSemaphoreWaiting = true;
                _threadsManager.acquireCalculationSemaphore();
            }
        }
    }

    private void stopThreads(PartialSumCalculator[] partialSumCalculators)
    {
        _threadsManager.IsCanceled = true;
        for (int i = 0; i < partialSumCalculators.length; i++)
        {
            partialSumCalculators[i].deinitialize();
        }
    }

    private void deinitializeArrays()
    {
        _array = null;
        _completedPartialSumsArray = null;
    }
}