namespace MissionPossible.Shared.Types
{
    public abstract class PagedQueryBase : IPagedQuery
    {
        public int Page { get; set; }
        public int Results { get; set; }
        public string OrderBy { get; set; }
        public string SortOrder { get; set; }

        protected PagedQueryBase(int page, int results, string orderBy)
        {
            Page = page;
            Results = results;
            OrderBy = orderBy;
        }
    }
}