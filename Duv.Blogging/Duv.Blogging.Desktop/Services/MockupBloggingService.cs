using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Duv.Blogging.Desktop.Models;
using System.Threading;

namespace Duv.Blogging.Desktop.Services
{
    class MockupBloggingService : IBloggingService
    {
        public IEnumerable<Blog> GetBlogs()
        {
            var blogs = new List<Blog>();
            for (var i = 0; i < 4; i++)
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(500));
                var name = string.Format("Blog {0}", i + 1);
                blogs.Add(new Blog()
                {
                    Name = name,
                    Url = string.Format("url://{0}", name),
                    Posts = new Post[] { 
                        new Post() { Title = string.Format("{0}/Post 1", name)},
                        new Post() { Title = string.Format("{0}/Post 2", name)},
                        new Post() { Title = string.Format("{0}/Post 3", name)},
                        new Post() { Title = string.Format("{0}/Post 4", name)},
                        new Post() { Title = string.Format("{0}/Post 5", name)},
                    }
                });
            }
            return blogs;
        }
    }
}
