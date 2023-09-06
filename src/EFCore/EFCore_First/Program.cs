using static EFCore_First.Context_Model;

namespace EFCore_First
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var db = new BloggingContext();

            Console.WriteLine($"Database path: {db.DbPath}.");

            // Create
            Console.WriteLine("Inserting a new blog");
            db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
            db.Add(new Blog { Url = "https://leeyeonjun85.github.io/" });
            db.SaveChanges();

            // Read
            Console.WriteLine("Querying for a blog");
            var blog = db.Blogs
                .OrderBy(b => b.BlogId)
                .First();

            var blog_my = db.Blogs
                .Where(b => b.Url == "https://leeyeonjun85.github.io/").FirstOrDefault();

            // Update
            Console.WriteLine("Updating the blog and adding a post");
            blog.Url = "https://devblogs.microsoft.com/dotnet";
            blog.Posts.Add(
                new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" });

            var my_post1 = new Post { Title = "AI 부트캠프를 마치고…", Content = "코드스테이츠와 함께했던 AI 부트캠프 회고" };
            var my_post2 = new Post { Title = "C# : Intro", Content = "C# 시작하기" };
            blog_my!.Posts.AddRange(
                new Post[] { my_post1, my_post2 });
            db.SaveChanges();

            // Delete
            Console.WriteLine("Delete the blog");
            db.Remove(blog);
            db.Remove(blog_my);
            db.SaveChanges();
        }
    }
}