using System.Collections.Generic;

namespace Csharp
{
    public class ObjectPool<T>
    {
        /*private readonly Queue<T> _objectPoolQueue;

        public int ObjectPoolCount => _objectPoolQueue.Count;

        public ObjectPool(int poolLength)
        {
            InstantiateObjectPool(poolLength);
        }

        public T GetObject()
        {
            return _objectPoolQueue.Count > 0 ? SelectObject() : CreateObject(true);
        }

        public void ReturnToPool(T objectToPool)
        {
            _objectPoolQueue.Enqueue(objectToPool);
        }

        private void InstantiateObjectPool(int poolLength)
        {
            for (int i = 0; i < poolLength; i++)
            {
                _objectPoolQueue.Enqueue(CreateObject(false));
            }
        }

        private T CreateObject(bool isActive)
        {
            T createdObject = new ();
            return createdObject;
        }

        private T SelectObject()
        {
            T selectedObject = _objectPoolQueue.Dequeue();
            return selectedObject;
        }*/
    }
}