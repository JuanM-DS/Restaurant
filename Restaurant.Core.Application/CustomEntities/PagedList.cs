namespace Restaurant.Core.Application.CustomEntities
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int TotalPage { get; set; }

        public int TotalCount { get; set; }

        public bool HasNextPage { get; set; }

        public bool HasPreviousPage { get; set; }

        public int? NextPage { get; set; }

        public int? PreviousPage { get; set; }

        public PagedList(IEnumerable<T> source, int totalCount, int pageSize, int page)
        {
            CurrentPage = page;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPage = (int)Math.Ceiling(totalCount / (double)pageSize);

            HasNextPage = page < TotalPage;
            HasPreviousPage = page > 1;
            NextPage = HasNextPage ? page + 1 : null; 
            PreviousPage = HasPreviousPage ? page - 1 : null;
            AddRange(source);
        }

        public static PagedList<T> Create(IEnumerable<T> source, int page, int pageSize)
        {
            int count = source.Count();
            source = source.Skip((page -1) * pageSize).Take(pageSize);
            return new(source, count, page, pageSize);
        }
    }
}
