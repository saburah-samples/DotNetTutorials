using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duv.Blogging.Desktop.ServiceAdapters
{
    public class ServiceAdapter<TService>
    {
        public Task<TResult> Execute<TResult>(Func<TService, TResult> command)
        {
            var task = new Task<TResult>(() =>
            {
                var service = ServiceLocator.GetService<TService>();
                return command.Invoke(service);
            });
            task.Start();
            return task;
        }
    }
}
