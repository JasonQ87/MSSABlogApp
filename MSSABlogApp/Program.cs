using ConsoleTables;
namespace MSSABlogApp
{
    internal class Program
    {
        static BloggingService bloggingService = new BloggingService();
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Blog App");
            Console.WriteLine("***********************");

            #region Blog Operations
            AddNewBlog();
            DisplayBlogs();
            UpdateBlog();
            DeleteBlog();
            #endregion

            #region Post Operations
            AddNewPost();
            DisplayPosts();
            UpdatePost();
            DeletePost();
            #endregion

            #region Advanced Queries
            // 1. Join: Get all posts with their corresponding blog names
            GetPostsWithBlogNames();

            #endregion

        }

        #region Blog Operations
        private static void AddNewBlog()
        {
            #region Adding New Blog
            Console.Write("Please Type New Blog Name: ");
            string blogName = Console.ReadLine();

            //Console.Write("Please Type Blog URL:");
            //string blogURL = Console.ReadLine();

            bloggingService.AddBlog(
                new Blog
                {
                    Name = blogName,
                    CreationDate = DateTime.Now,
                });

            Console.WriteLine("New Blog Added Successfully!\n");
            Console.WriteLine();
            #endregion
        }
        private static void DisplayBlogs()
        {
            #region Display All Blogs
            Console.WriteLine("List of Blogs");
            Console.WriteLine("*************");
            var table = new ConsoleTable("Id", "Name", "Creation Date");
            foreach (Blog blog in bloggingService.GetAllBlogs())
            {
                table.AddRow(blog.BlogId, blog.Name, blog.CreationDate);
            }
            table.Write();
            #endregion
        }
        private static void UpdateBlog()
        {
            #region Update Blog Data
            Console.WriteLine("Please Type Blog ID you would like to update: ");
            int blogID = int.Parse(Console.ReadLine());

            Blog blogFound = bloggingService.GetBlogByID(blogID);
            if (blogFound != null)
            {
                Console.Write("Please type updated Blog name: ");
                blogFound.Name = Console.ReadLine();
                bloggingService.UpdateBlog(blogFound);
                Console.WriteLine("Blog update complete!");
            }
            else
            {
                Console.Write("Sorry, No Blog found.  Please try again.");
            }
            #endregion
        }
        private static void DeleteBlog()
        {
            #region Delete Blog Data
            Console.WriteLine("Please Type Blog ID to delete: ");
            int blogID = int.Parse(Console.ReadLine());

            Blog blogFound = bloggingService.GetBlogByID(blogID);
            if (blogFound != null)
            {
                bloggingService.DeleteBlog(blogFound);
                Console.Write("Blog deleted!");
            }
            else
            {
                Console.Write("Sorry, No Blog found.  Please try again.");
            }
            #endregion
        }
        #endregion

        #region Post Operations
        static public void AddNewPost()
        {
            #region Adding New Post
            Console.Write("Please Type New Post Title: ");
            string postTitle = Console.ReadLine();

            Console.Write("Please Type New Post Content: ");
            string postContent = Console.ReadLine();

            Console.WriteLine("Please Type Blog ID: ");
            int blogID = int.Parse(Console.ReadLine());

            bloggingService.AddPost(
                new Post
                {
                    Title = postTitle,
                    Content = postContent,
                    BlogId = blogID,
                    CreationDate = DateTime.Now,
                });

            Console.WriteLine("New Post Added Successfully!");
            Console.WriteLine();
            #endregion
        }
        private static void DisplayPosts()
        {
            #region Display All Posts
            Console.WriteLine("List of all Posts");
            Console.WriteLine("*****************");
            var table = new ConsoleTable("Id", "Title", "Content", "Creation Date");
            foreach (Post post in bloggingService.GetAllPost())
            {
                table.AddRow(post.PostId, post.Title, post.Content, post.CreationDate);
            }
            table.Write();
            #endregion
        }
        static public void UpdatePost()
        {
            #region Update Post Data
            Console.WriteLine("Please Type Post ID you would like to update: ");
            int postID = int.Parse(Console.ReadLine());

            Post? postFound = bloggingService.GetPostByID(postID);
            if (postFound != null)
            {
                Console.Write("Please type updated Post Content: ");
                postFound.Content = Console.ReadLine();
                bloggingService.UpdatePost(postFound);
                Console.WriteLine("Post update complete!");
            }
            else
            {
                Console.Write("Sorry, No Post found.  Please try again.");
            }
            #endregion
        }
        private static void DeletePost()
        {
            #region Delete Blog Data
            Console.WriteLine("Please Type Post ID to delete: ");
            int postID = int.Parse(Console.ReadLine());

            Post? postFound = bloggingService.GetPostByID(postID);
            if (postFound != null)
            {
                bloggingService.DeletePost(postFound);
                Console.Write("Post deleted!");
            }
            else
            {
                Console.Write("Sorry, No Post found.  Please try again.");
            }
            #endregion
        }
        #endregion

        #region Advanced Queries
        public static void GetPostsWithBlogNames()
        {
            Console.WriteLine("List Of Posts");
            Console.WriteLine("***************");
            var table = new ConsoleTable("Title", "Content", "Creation Date", "Blog Name");

            foreach (PostWithBlogDataDTO post in bloggingService.GetPostsWithBlogName())
            {
                table.AddRow(post.Title, post.Content, post.CreationDate, post.BlogName);
            }
            table.Write();
        }

        public static void GetBlogsHavingMoreThanOnePost()
        {
            Console.WriteLine("Summary: Number Of Posts Per Blog ");
            Console.WriteLine("*********************************");
            var table = new ConsoleTable("Blog ID", "Name", "Posts Count");
            foreach (dynamic summaryObj in bloggingService.GetBlogsHavingMoreThanOnePost(0))
            {
                table.AddRow(summaryObj.BlogId, summaryObj.Name, summaryObj.Count);
            }
            table.Write();
        }
        #endregion

    }
}
