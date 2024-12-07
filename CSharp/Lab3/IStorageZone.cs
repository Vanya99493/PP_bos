namespace Lab3
{
	public interface IStorageZone
	{
		public int Id { get; }

		void OccupyAccessToStorageZone();
		void ReleaseAccessToStorageZone();
		void OccupyPushItemSemaphore();
		void ReleasePushItemSemaphore();
		void OccupyPickItemSemaphore();
		void ReleasePickItemSemaphore();
		void PushItem(string item);
		string PickItem();
	}
}