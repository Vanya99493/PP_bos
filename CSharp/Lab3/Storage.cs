namespace Lab3
{
	public class Storage
	{
		private IStorageZone[] _storageZones;

		public Storage(int storageSize, int storageDoors)
		{
			_storageZones = new StorageZone[storageDoors];

			int overSizeValue = 0;
			int storageZoneBaseSize = storageSize / _storageZones.Length;
			int storageZoneSize;
			for (int i = 0; i < _storageZones.Length; i++)
			{
				storageZoneSize = storageZoneBaseSize;
				if((storageSize % _storageZones.Length) - overSizeValue > 0)
				{
					overSizeValue++;
					storageZoneSize += 1;
				}
				_storageZones[i] = new StorageZone(storageZoneSize, i);
			}
		}

		public IStorageZone GetSoragetZoneByIndex(int storageZoneIndex)
		{
			return _storageZones[storageZoneIndex];
		}

		public void PushItem(int storageZoneIndex, string item)
		{
			_storageZones[storageZoneIndex].PushItem(item);
		}

		public string PickItem(int storageZoneIndex)
		{
			return _storageZones[storageZoneIndex].PickItem();
		}
	}
}