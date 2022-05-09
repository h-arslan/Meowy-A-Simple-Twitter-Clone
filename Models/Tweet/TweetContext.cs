using Microsoft.EntityFrameworkCore;

namespace Meowy_deneme.Models
{
    public class TweetContext : DbContext
    {
        public TweetContext(DbContextOptions<TweetContext> options)
            : base(options)
        {
        }

        public DbSet<Tweet> Tweets { get; set; } = null!;

    }
}
