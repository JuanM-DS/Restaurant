namespace Restaurant.Core.Application.QueryFilters
{
    public class DishQueryFilters
    {
        public string? Name { get; set; }

        public decimal? Price { get; set; }

        public int? Portions { get; set; }

        public int? CategoryId { get; set; }

        public int? Page { get; set; }

        public int? PageSize { get; set; }
    }
}
