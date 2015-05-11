using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duv.Blogging.Desktop.Models
{
    public class Person
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public IEnumerable<Entry> Entries { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
    }
}
