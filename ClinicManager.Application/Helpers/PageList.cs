using MediatR;

namespace ClinicManager.Application.Helpers
{
    public class PageList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public PageList()
        { }

        public PageList(List<T> items, int currentPage, int pageSize, int totalCount)
        {
            TotalCount = totalCount;
            PageSize = pageSize;
            CurrentPage = currentPage;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            AddRange(items);
        }

        public static PageList<T> CreatePagination(IQueryable<T> query, int pageNumber, int pageSize) { 
            var result = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PageList<T>(result, pageNumber, pageSize, result.Count);
        }
            
    }
}
