namespace Restaurant.Core.Domain.Settings
{
    public class EmailSettings
    {
        public string EmailFrom { get; set; } = null!;

        public string StmpHost { get; set; } = null!;

        public int StmpPort { get; set; }

        public string StmpPassword { get; set; } = null!;

        public string StmpUser { get; set; } = null!;

        public string DisplayName { get; set; } = null!;
    }
}
