using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duv.Blogging.Desktop.Models
{
    public class Blog
    {
        public string Name { get; set; }
        public string Url { get; set; }

        public IEnumerable<Post> Posts { get; set; }
    }
}
