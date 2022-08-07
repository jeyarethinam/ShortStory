using Microsoft.EntityFrameworkCore;
using ShortStory.Entities;

namespace ShortStory.DataContext
{
    public class ShortStoryDbContext : DbContext
    {
        public ShortStoryDbContext(DbContextOptions<ShortStoryDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; } = null!;
        public DbSet<Post> Post { get; set; } = null!;

        public DbSet<UserFollowers> UserFollowers { get; set; } = null!;
        public DbSet<VowelStats> VowelStats { get; set; } = null!;




    }
}
