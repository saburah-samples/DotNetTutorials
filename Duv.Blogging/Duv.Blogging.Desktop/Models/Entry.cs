using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duv.Blogging.Desktop.Models
{
    public abstract class Entry
    {
        public string Content { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public Person Author { get; set; }
    }
}
