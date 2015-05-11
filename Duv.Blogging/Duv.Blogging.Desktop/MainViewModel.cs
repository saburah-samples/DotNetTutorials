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
        private IEnumerable<Blog> blogs;
        private bool isInLoading;

        public MainViewModel()
        {
            Blogs = new ObservableCollection<Blog>();
            FirstBlogCommand = new RelayCommand(SelectFirstBlog, CanSelectFirstBlog);
            RefreshBlogsCommand = new RelayCommand(RefreshBlogs, CanRefreshBlogs);
            LoadData();
        }

        private bool CanSelectFirstBlog(object obj)
        {
            return IsInLoading == false && Blogs.Count() > 0;
        }

        private void SelectFirstBlog(object obj)
        {
            SelectedBlog = Blogs.FirstOrDefault();
        }

        private bool CanRefreshBlogs(object obj)
        {
            return IsInLoading == false;
        }

        private void RefreshBlogs(object obj)
        {
            ClearData();
            LoadData();
        }

        private void ClearData()
        {
            Blogs = new ObservableCollection<Blog>();
        }

        private async void LoadData()
        {
            var service = ServiceLocator.BloggingService;

            IsInLoading = true;
            var data = await service.GetBlogsAsync();
            Blogs = new ObservableCollection<Blog>(data);
            SelectedBlog = Blogs.FirstOrDefault();
            IsInLoading = false;
        }

        public IEnumerable<Blog> Blogs
        {
            get { return blogs; }
            private set { SetPropertyAndNotify(ref blogs, value); }
        }

        public Blog SelectedBlog
        {
            get { return selectedBlog; }
            set { SetPropertyAndNotify(ref selectedBlog, value); }
        }

        public bool IsInLoading
        {
            get { return isInLoading; }
            set { SetPropertyAndNotify(ref isInLoading, value); }
        }

        public ICommand FirstBlogCommand { get; private set; }

        public ICommand RefreshBlogsCommand { get; private set; }
    }
}
