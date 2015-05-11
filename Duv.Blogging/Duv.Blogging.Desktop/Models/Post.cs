using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Duv.Blogging.Desktop.Models
{
    public class Post : Entry
    {
        public string Title { get; set; }
        public string PermaLink { get; set; }

        public Blog Blog { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}
