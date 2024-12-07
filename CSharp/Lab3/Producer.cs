namespace Lab3
{
	public class Producer
	{
		private string _name;
		private int _itemsCount;
		private int _pushDelay_ms;
		private Queue<IStorageZone> _storageZones;

		public Producer(string name, int itemsCount, int pushDelay_ms, List<IStorageZone> storageZones)
		{
			_name = name;
			_itemsCount = itemsCount;
			_pushDelay_ms = pushDelay_ms;

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

				storageZone.OccupyPushItemSemaphore();
				Thread.Sleep(_pushDelay_ms);
				storageZone.OccupyAccessToStorageZone();

				string item = $"Item {Program.IDController.GetId(storageZone.Id)}";
				storageZone.PushItem(item);
				Console.WriteLine($"{_name} push {item}");

				storageZone.ReleaseAccessToStorageZone();
				storageZone.ReleasePickItemSemaphore();

				_storageZones.Enqueue(storageZone);
			}
		}
	}
}