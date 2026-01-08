using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSSABlogApp
{
    public class PostWithBlogDataDTO
    {
        //Data Transfer Object Class
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public string BlogName { get; set; }
    }
}
