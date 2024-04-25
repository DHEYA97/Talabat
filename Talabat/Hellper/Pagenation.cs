namespace Talabat.APIs.Hellper
{
	public class Pagenation<T>
	{
		public Pagenation(int? pageSize, int pageIndex, int count, IReadOnlyList<T> items)
		{
			PageSize = pageSize;
			PageIndex = pageIndex;
			Count = count;
			Items = items;
		}

		public int? PageSize { get; set; }
		public int PageIndex { get; set; }
		public int Count { get; set; }
		public IReadOnlyList<T> Items { get; set; }

	}
}
