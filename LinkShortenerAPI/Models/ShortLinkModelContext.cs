using Microsoft.EntityFrameworkCore;

namespace LinkShortenerAPI.Models
{
    public class ShortLinkModelContext : DbContext
    {
        public ShortLinkModelContext(DbContextOptions dbContextOptions) : base(dbContextOptions) {}

        public DbSet<ShortLinkModel> ShortLinks { get; set; }
    }
}
