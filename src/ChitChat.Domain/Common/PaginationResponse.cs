namespace ChitChat.Domain.Common
{
    public class PaginationResponse<T>
    {
        public PaginationResponse()
        {
            Items = new List<T>();
        }

        public int TotalCount { get; set; }

        public IEnumerable<T> Items { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public virtual List<string> SortableFields { get; set; } = new();
    }
}
