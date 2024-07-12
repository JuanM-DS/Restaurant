namespace Restaurant.Core.Application.QueryFilters
{
    public class OrderQueryFilters
    {
        public decimal? Subtotal { get; set; }

        public string? UserId { get; set; }

        public int? TableId { get; set; }

        public int? StatusId { get; set; }

        public int? Page { get; set; }

        public int? PageSize { get; set; }
    }
}
