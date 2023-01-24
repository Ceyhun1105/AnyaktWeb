namespace ExamBilet2.Helpers
{
    public class PaginatedList<T> : List<T>
    {
        public PaginatedList(List<T> values , int count, int pageSize , int page )
        {
            this.AddRange(values);
            ActivePage = page;
            TotalPageCount = (int)Math.Ceiling(count/(double)pageSize);
        }

        public int TotalPageCount { get; set; }
        public int ActivePage { get; set; }
        public bool Next { get;  } 
        public bool Previous { get;  }


        public static PaginatedList<T> Create (IQueryable<T> query , int pageSize ,int page)
        {
            return new PaginatedList<T>(query.Skip((page - 1) * pageSize).Take(pageSize).ToList(), query.Count(), pageSize, page);
        }
    }
}
