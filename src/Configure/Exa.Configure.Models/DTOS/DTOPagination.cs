namespace Exa.Configure.Models.DTOS
{
    public class DTOPagination<TEntity>
    {
        public int ActivePage { get; set; } = 0;
        public int PageSize { get; set; } = 0;
        public int TotalCount { get; set; } = 0;
        public int TotalPageCount { get; set; } = 0;
        public List<TEntity> Data { get; set; } = new List<TEntity>();
    }
}
