public class PartialCalculator extends Thread
{
    private Calculator _calculator;
    private int[] _array;
    private int _startIndex;
    private int _step;

    public PartialCalculator(Calculator calculator, int[] array, int startIndex, int step)
    {
        _calculator = calculator;
        _array = array;
        _startIndex = startIndex;
        _step = step;
    }

    @Override
    public void run()
    {
        long sum = 0;
        for (int i = _startIndex; i < _array.length; i+= _step)
        {
            sum += _array[i];
        }
        _calculator.addSum(sum);
    }
}