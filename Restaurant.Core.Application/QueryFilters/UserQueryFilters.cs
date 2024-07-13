namespace Restaurant.Core.Application.QueryFilters
{
    public class UserQueryFilters
    {
        public string? FirstName { get; set; } 

        public string? LastName { get; set; } 

        public string? UserName { get; set; } 

        public string? Email { get; set; } 

        public bool? EmailConfirmed { get; set; }

        public int? Page { get; set; }

        public int? PageSize { get; set; }
    }
}
