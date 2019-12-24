using EFGetStarted.Model;
using Microsoft.EntityFrameworkCore;

namespace EFGetStarted
{
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }


        public BloggingContext(DbContextOptions<BloggingContext> options)
            : base(options)
        {
        }
    }
}
