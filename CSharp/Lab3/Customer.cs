namespace Lab3
{
	public class Customer
	{
		private string _name;
		private int _itemsCount;
		private int _pickDelay_ms;
		private Queue<IStorageZone> _storageZones;

		public Customer(string name, int itemsCount, int pickDelay_ms, List<IStorageZone> storageZones)
		{
			_name = name;
			_itemsCount = itemsCount;
			_pickDelay_ms = pickDelay_ms;

			_storageZones = new Queue<IStorageZone>();
			foreach (var storageZone in storageZones)
			{
				_storageZones.Enqueue(storageZone);
			}
		}

		public void Start()
		{
			for (int i = 0; i < _itemsCount; i++)
			{
				var storageZone = _storageZones.Dequeue();

				storageZone.OccupyPickItemSemaphore();
				Thread.Sleep(_pickDelay_ms);
				storageZone.OccupyAccessToStorageZone();

				string item = storageZone.PickItem();
				Console.WriteLine($"{_name} pick {item}");

				storageZone.ReleaseAccessToStorageZone();
				storageZone.ReleasePushItemSemaphore();

				_storageZones.Enqueue(storageZone);
			}
		}
	}
}