using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Duv.TaskManager.Domain.Model
{
    public interface IUserActionLogger
    {
        void LogCreateProject(Project project);

        void LogDeleteTask(Project project, string p);

        void LogCloseProject(Project project);

        void LogReopenProject(Project project);
    }
}
