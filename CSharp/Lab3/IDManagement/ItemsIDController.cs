namespace Lab3.IDManagement
{
    public class ItemsIDController : IIDController
    {
        private Semaphore[] _idThreadsAccesses;
        private int[] _threads;
        private int _step;

        public ItemsIDController(int startID, int threadsCount)
        {
            _threads = new int[threadsCount];
            _step = threadsCount;

            for (int i = 0; i < threadsCount; i++)
            {
                _threads[i] = i + startID;
            }

			_idThreadsAccesses = new Semaphore[threadsCount];
			for (int i = 0; i < threadsCount; i++)
			{
				_idThreadsAccesses[i] = new Semaphore(1, 1);
			}
		}

        public int GetId(int threadIndex)
        {
            _idThreadsAccesses[threadIndex].WaitOne();

            int newId = _threads[threadIndex];
            _threads[threadIndex] += _step;

            _idThreadsAccesses[threadIndex].Release();

            return newId;
		}
	}
}