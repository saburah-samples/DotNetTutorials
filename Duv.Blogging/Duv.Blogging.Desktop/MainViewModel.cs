using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Duv.Blogging.Desktop.Infrastructure.Mvvm;
using Duv.Blogging.Desktop.Models;
using Duv.Blogging.Desktop.Services;

namespace Duv.Blogging.Desktop
{
    class MainViewModel : BindableBase
    {
        private Blog selectedBlog;
        public MainViewModel()
        {
            Blogs = new ObservableCollection<Blog>();
            FirstBlogCommand = new RelayCommand(SelectFirstBlog, CanSelectFirstBlog);
            LoadData();
        }

        private bool CanSelectFirstBlog(object obj)
        {
            return Blogs.Count > 0;
        }

        private void SelectFirstBlog(object obj)
        {
            SelectedBlog = Blogs.FirstOrDefault();
        }

        private void LoadData()
        {
            var service = ServiceLocator.BloggingService;
            Blogs = new ObservableCollection<Blog>(service.GetBlogs());
            SelectedBlog = Blogs.FirstOrDefault();
        }

        public ICollection<Blog> Blogs { get; private set; }
        public Blog SelectedBlog
        {
            get { return selectedBlog; }
            set { SetPropertyAndNotify(ref selectedBlog, value); }
        }
        public ICommand FirstBlogCommand { get; set; }
    }
}
