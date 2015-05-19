using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Duv.Blogging.Desktop.Models;

namespace Duv.Blogging.Desktop.Services
{
    public interface IBloggingService
    {
        IEnumerable<Blog> GetBlogs();
    }
}
