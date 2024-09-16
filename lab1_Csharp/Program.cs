using System;

namespace lab1_Csharp
{
    internal class Program
    {
        static int _threadsCount = 5;
        static int _arrayLength = 1000000;

        private static void Main(string[] args)
        {
            float calculationTime;

            Calculator calculator = new Calculator();
            int[] array = CreateArray(_arrayLength);
            long sum = 0;

            Console.WriteLine("Single thread:");
            sum = calculator.SumInMultipleThreads(array, 1, out calculationTime);
            PrintCalculationData(sum, calculationTime);

            Console.WriteLine("Multuiple threads:");
            sum = calculator.SumInMultipleThreads(array, _threadsCount, out calculationTime);
            PrintCalculationData(sum, calculationTime);

            Console.ReadKey();
        }

        private static int[] CreateArray(int arrayLength)
        {
            int[] array = new int[_arrayLength];
            for (int i = 0; i < arrayLength; i++) 
            {
                array[i] = i;
            }
            return array;
        }

        private static void PrintCalculationData(long sum, float calculationTime)
        {
            Console.WriteLine($"Sum: {sum}\nCalculation time: {calculationTime} ms");
        }
    }
}