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
using Duv.Blogging.Desktop.ServiceAdapters;

namespace Duv.Blogging.Desktop
{
    class MainViewModel : BindableBase
    {
        private Blog selectedBlog;
        private IEnumerable<Blog> blogs;
        private bool isInLoading;
        private readonly ServiceAdapter<IBloggingService> bloggingServiceAdapter;
        private string errorMessage;

        public MainViewModel()
        {
            bloggingServiceAdapter = new ServiceAdapter<IBloggingService>();

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
            SelectedBlog = null;
        }

        private async void LoadData()
        {
            IsInLoading = true;
            try
            {
                var data = await bloggingServiceAdapter.Execute(s => s.GetBlogs());
                Blogs = new ObservableCollection<Blog>(data);
                SelectedBlog = Blogs.FirstOrDefault();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                ClearData();
            }
            finally
            {
                IsInLoading = false;
                CommandManager.InvalidateRequerySuggested();
            }
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

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetPropertyAndNotify(ref errorMessage, value); }
        }

        public ICommand FirstBlogCommand { get; private set; }

        public ICommand RefreshBlogsCommand { get; private set; }
    }
}
