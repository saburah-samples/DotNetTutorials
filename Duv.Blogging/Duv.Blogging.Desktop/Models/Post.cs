using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Duv.Blogging.Desktop.Models
{
    public class Post
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public Blog Blog { get; set; }
    }
}
