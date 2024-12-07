namespace Lab3
{
	public class StorageZone : IStorageZone
	{
		private Semaphore _pushItemSemaphore;
		private Semaphore _pickItemSemaphore;
		private Semaphore _accessToStorageZoneSemaphore;

		private List<string> _storage;

		public int Id { get; private set; }

		public StorageZone(int storageSize, int id)
		{
			_pushItemSemaphore = new Semaphore(storageSize, storageSize);
			_pickItemSemaphore = new Semaphore(0, storageSize);
			_accessToStorageZoneSemaphore = new Semaphore(1, 1);

			_storage = new List<string>();

			Id = id;
		}

		public void PushItem(string item)
		{
			_storage.Add(item);
		}

		public string PickItem()
		{
			string item = _storage[0];
			_storage.RemoveAt(0);
			return item;
		}

		public void OccupyAccessToStorageZone() => _accessToStorageZoneSemaphore.WaitOne();
		public void ReleaseAccessToStorageZone() => _accessToStorageZoneSemaphore.Release();

		public void OccupyPushItemSemaphore() => _pushItemSemaphore.WaitOne();
		public void ReleasePushItemSemaphore() => _pushItemSemaphore.Release();

		public void OccupyPickItemSemaphore() => _pickItemSemaphore.WaitOne();
		public void ReleasePickItemSemaphore() => _pickItemSemaphore.Release();
	}
}