using EFGetStarted.Model;
using System;
using System.Linq;

namespace EFGetStarted
{
    public class Worker
    {
        private readonly BloggingContext _dbContext;

        public Worker(BloggingContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Run()
        {
            // Create
            Console.WriteLine("Inserting a new blog");
            _dbContext.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
            _dbContext.SaveChanges();

            // Read
            Console.WriteLine("Querying for a blog");
            var blog = _dbContext.Blogs
                .OrderBy(b => b.BlogId)
                .First();

            // Update
            Console.WriteLine("Updating the blog and adding a post");
            blog.Url = "https://devblogs.microsoft.com/dotnet";
            blog.Posts.Add(
                new Post
                {
                    Title = "Hello World",
                    Content = "I wrote an app using EF Core!"
                });
            _dbContext.SaveChanges();

            // Delete
            Console.WriteLine("Delete the blog");
            _dbContext.Remove(blog);
            _dbContext.SaveChanges();

        }
    }
}
