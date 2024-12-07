using Lab3.IDManagement;

namespace Lab3
{
	public class Program
	{
		public static IIDController IDController;

		private static int _storageSize = 10;
		private static int _storageZonesCount = 5;
		private static int _itemsCount = 41;

		private static int _pickDelay_ms = 1000;
		private static int _pushDelay_ms = 0;

		private static int _producersCount = 5;
		private static int _customersCount = 5;

		private static void Main(string[] args)
		{
			Storage storage = new Storage(_storageSize, _storageZonesCount);
			IDController = new ItemsIDController(1, _storageZonesCount);

			InitializeProducers(storage);
			InitializeCustomers(storage);
			
			Console.ReadKey();
		}

		private static void InitializeProducers(Storage storage)
		{
			int overSizeValue = 0;
			int baseItemsCountOfOneProducer = _itemsCount / _producersCount;
			int itemsCountOfOneProducer = baseItemsCountOfOneProducer;
			var distributedZones = DistributeZtorageZones(storage, _storageZonesCount, _producersCount);
			for (int i = 0; i < _producersCount; i++)
			{
				itemsCountOfOneProducer = baseItemsCountOfOneProducer;
				if((_itemsCount % _producersCount) - overSizeValue > 0)
				{
					overSizeValue++;
					itemsCountOfOneProducer++;
				}
				string producerName = $"Producer {i + 1}";
				new Thread(new Producer(producerName, itemsCountOfOneProducer, _pushDelay_ms, distributedZones[i]).Start).Start();
			}
        }

		private static void InitializeCustomers(Storage storage)
		{
			int overSizeValue = 0;
			int baseItemsCountOfOneCustomer = _itemsCount / _customersCount;
			int itemsCountOfOneCustomer = baseItemsCountOfOneCustomer;
			var distributedZones = DistributeZtorageZones(storage, _storageZonesCount, _customersCount);
			for (int i = 0; i < _customersCount; i++)
			{
				itemsCountOfOneCustomer = baseItemsCountOfOneCustomer;
				if((_itemsCount % _customersCount) - overSizeValue > 0)
				{
					overSizeValue++;
					itemsCountOfOneCustomer++;
				}
				string customerName = $"Customer {i + 1}";
				new Thread(new Customer(customerName, _itemsCount, _pickDelay_ms, distributedZones[i]).Start).Start();
			}
        }

		private static List<IStorageZone>[] DistributeZtorageZones(Storage storage, int storageZonesCount, int countOfDistribute)
		{
			List<IStorageZone>[] distributedZones = new List<IStorageZone>[countOfDistribute];

			for (int i = 0; i < distributedZones.Length; i++)
			{
				distributedZones[i] = new List<IStorageZone>();

				for (int j = i % storageZonesCount; j < storageZonesCount; j += distributedZones.Length)
				{
					distributedZones[i].Add(storage.GetSoragetZoneByIndex(j));
				}
			}

			return distributedZones;
		}
	}
}