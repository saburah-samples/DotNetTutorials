using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duv.Blogging.Desktop.Models
{
    public class Comment : Entry
    {
        public Post Post { get; set; }
    }
}
