public class Main
{
    static int _threadsCount = 5;
    static int _arrayLength = 1000000;

    public static void main(String[] args)
    {
        Calculator calculator = new Calculator();
        int[] array = createArray(_arrayLength);

        long sum = calculator.sumInMultipleThreads(array, _threadsCount);

        printCalculationData(sum);
    }

    public static int[] createArray(int arrayLength)
    {
        int[] array = new int[arrayLength];
        for (int i = 0; i < array.length; i++)
        {
            array[i] = i;
        }
        return array;
    }

    public static void printCalculationData(long sum)
    {
        System.out.println(sum);
    }
}