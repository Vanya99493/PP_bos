using System;

namespace Csharp
{
    public class Program
    {
        private static void Main(string[] args)
        {

        }

        private static int[] CreateArray(int arrayLength)
        {
            int[] array = new int[arrayLength];

            for (int i = 0; i < arrayLength; i++)
            {
                array[i] = i;
            }

            return array;
        }

        private static void PrintSum(long sum)
        {
            Console.WriteLine($"Sum: {sum}");
        }
    }
}