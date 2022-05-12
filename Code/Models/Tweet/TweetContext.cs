using Microsoft.EntityFrameworkCore;

namespace Meowy.Models.Tweet
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
