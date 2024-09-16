namespace lab1_Csharp
{
    public class PartialCalculator
    {
        private int[] _array;
        private int _startIndex;
        private int _step;

        public long Sum { get; private set; }

        public PartialCalculator(int[] array, int startIndex, int step)
        {
            _array = array;
            _startIndex = startIndex;
            _step = step;
        }

        public void Calculate()
        {
            for (int i = _startIndex; i < _array.Length; i += _step)
            {
                Sum += _array[i];
            }
        }
    }
}