public class Main
{
    private static int _arrayLength = 2000000;
    private static int _threadsCount = 12;

    public static void main(String[] args)
    {
        long[] array = createArray(_arrayLength);
        SumCalculator calculator = new SumCalculator();
        long sum = calculator.calculateSum(array, _threadsCount);

        System.out.println("Sum: " + sum);
    }

    private static long[] createArray(int length)
    {
        long[] array = new long[length];
        for (int i = 0; i < length; i++)
        {
            array[i] = i;
        }
        return array;
    }
}