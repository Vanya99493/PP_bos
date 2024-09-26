using System;

namespace Csharp
{
    public class Program
    {
        private static int _arrayLength = 1000000;
        private static int _threadsCount = 12;

        private static void Main(string[] args)
        {
            long[] array = CreateArray(_arrayLength);
            SumCalculator calculator = new SumCalculator();
            long sum = calculator.CalculateSum(array, _threadsCount, out var calculationTime);

            Console.WriteLine($"Sum: {sum}\nCalculation time: {calculationTime} s");
            Console.ReadKey();
        }

        private static long[] CreateArray(int arrayLength)
        {
            long[] array = new long[arrayLength];
            for (long i = 0; i < arrayLength; i++)
            {
                array[i] = i;
            }
            return array;
        }
    }
}