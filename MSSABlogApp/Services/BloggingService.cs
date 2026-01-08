using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;

namespace MSSABlogApp
{
    public class BloggingService
    {
        private readonly BloggingContext _context;

        public BloggingService()
        {
            this._context = new BloggingContext();
        }

        #region Blog CRUD Operations
        public void AddBlog(Blog blog)
        {
            if (blog ==null)
                throw new ArgumentNullException("Blog is empty");
             
            this._context.Blogs.Add(blog);
            this._context.SaveChanges();
            
        }

        public List<Blog> GetAllBlogs()
        {
            // Recall: select * from blogs (SQL)

            // LINQ Query
            // Construct Query Statement in C# using LINQ
            IQueryable<Blog> query = from blog in this._context.Blogs
                             select blog;

            //Execute the Query statement
            List<Blog> blogs = query.ToList();
            return blogs;
        }

        public Blog? GetBlogByID(int id)
        {
            // LINQ Query
            // Construct Query Statement in C# using LINQ
            IQueryable<Blog> query = from blog in this._context.Blogs
                                     where blog.BlogId==id
                                     select blog;

            //Execute the Query statement
            Blog? blogOBJ = query.FirstOrDefault();
            return blogOBJ;
        }

        public void UpdateBlog(Blog newBlog)
        {
            this._context.Blogs.Update(newBlog);
            this._context.SaveChanges();
        }

        public void DeleteBlog(Blog blog)
        {
            this._context.Blogs.Remove(blog);
            this._context.SaveChanges();
        }

        #endregion

        #region Post CRUD Operations
        public void AddPost(Post post)
        {
            if (post == null)
                throw new ArgumentNullException("Post is empty");
            
            
             this._context.Posts.Add(post);
             this._context.SaveChanges();
            
        }
        public List<Post> GetAllPost()
        {
            //LinQ Methods
            List<Post> posts = this._context.Posts.ToList();
            return posts;
        }
        public Post? GetPostByID(int id)
        {
            Post? post = this._context.Posts
                                    .Where(post => post.PostId == id)
                                    .FirstOrDefault();
            return post;
        }
        public void UpdatePost(Post newPost)
        {
            this._context.Posts.Update(newPost);
            this._context.SaveChanges();
        }

        public void DeletePost(Post post)
        {
            this._context.Posts.Remove(post);
            this._context.SaveChanges();
        }

        #endregion

        #region Advanced Queries
        public List<PostWithBlogDataDTO> GetPostsWithBlogName()
        {
            //LinQ Query
            IQueryable<PostWithBlogDataDTO> query = from post in this._context.Posts
                                        join blog in this._context.Blogs
                                        on post.BlogId equals blog.BlogId
                                        orderby post.Title ascending
                                        select new PostWithBlogDataDTO
                                        {
                                            Title=post.Title,
                                            Content=post.Content,
                                            CreationDate=post.CreationDate,
                                            BlogName=blog.Name
                                            
                                        };
            return query.ToList();
        }

        // Get blogs that have more than 1 post
        public dynamic GetBlogsHavingMoreThanOnePost(int count)
        {
            var query = from blog in _context.Blogs
                        join post in _context.Posts on blog.BlogId equals post.BlogId
                        group post by blog.BlogId into blogGroup
                        where blogGroup.Count() > count
                        select new
                        {
                            BlogId = blogGroup.Key,
                            Name = blogGroup.First().Blog.Name,
                            Count = blogGroup.Count()
                        };
            return query.ToList();
        }
        #endregion

    }
}

