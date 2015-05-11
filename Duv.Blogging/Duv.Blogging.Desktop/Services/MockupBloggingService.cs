using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Duv.Blogging.Desktop.Models;

namespace Duv.Blogging.Desktop.Services
{
    class MockupBloggingService : IBloggingService
    {
        public async Task<IEnumerable<Blog>> GetBlogsAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(3)).ConfigureAwait(false);
            return GetBlogs();
        }

        public IEnumerable<Blog> GetBlogs()
        {
            var blogs = new List<Blog>();
            blogs.Add(new Blog()
            {
                Name = "Blog 1",
                Url = "url://Blog 1",
                Posts = new Post[] { 
                    new Post() { Title = "Blog 1/Post 1"},
                    new Post() { Title = "Blog 1/Post 2"},
                    new Post() { Title = "Blog 1/Post 3"},
                    new Post() { Title = "Blog 1/Post 4"}
                }
            });
            blogs.Add(new Blog()
            {
                Name = "Blog 2",
                Url = "url://Blog 2",
                Posts = new Post[] { 
                    new Post() { Title = "Blog 2/Post 1"},
                    new Post() { Title = "Blog 2/Post 2"},
                    new Post() { Title = "Blog 2/Post 3"},
                    new Post() { Title = "Blog 2/Post 4"}
                }
            });
            blogs.Add(new Blog()
            {
                Name = "Blog 3",
                Url = "url://Blog 3",
                Posts = new Post[] { 
                    new Post() { Title = "Blog 3/Post 1"},
                    new Post() { Title = "Blog 3/Post 2"},
                    new Post() { Title = "Blog 3/Post 3"},
                    new Post() { Title = "Blog 3/Post 4"}
                }
            });
            blogs.Add(new Blog()
            {
                Name = "Blog 4",
                Url = "url://Blog 4",
                Posts = new Post[] { 
                    new Post() { Title = "Blog 4/Post 1"},
                    new Post() { Title = "Blog 4/Post 2"},
                    new Post() { Title = "Blog 4/Post 3"},
                    new Post() { Title = "Blog 4/Post 4"}
                }
            });
            return blogs;
        }
    }
}
