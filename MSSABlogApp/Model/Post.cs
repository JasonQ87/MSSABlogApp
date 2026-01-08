using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSSABlogApp
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public int BlogId { get; set; } // Foreign key (1 to 1 relationship with Blog)
        public virtual Blog Blog { get; set; } // Navigational Property to Blog
    }
}
