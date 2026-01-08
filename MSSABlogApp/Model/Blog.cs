using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSSABlogApp
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }


        public virtual List<Post> Posts { get; set; } //Navigational Property (from Blog to all its child posts)
    }

}
