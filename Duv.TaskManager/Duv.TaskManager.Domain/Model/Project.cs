using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Duv.TaskManager.Domain.Model
{
    public class Project
    {
        private static readonly NLog.Logger Log = NLog.LogManager.GetLogger(typeof(Project).FullName);

        /// <summary>
        /// Parameterless constructor
        /// </summary>
        public Project()
        {
            Tasks = new List<Task>();
            Logs = new List<ProjectLogEntry>();

            if (Log.IsDebugEnabled) Log.Debug("Project parameterless constructor");
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Project name</param>
        /// <param name="description">Description</param>
        /// <param name="logger">Logger</param>
        public Project(string name, string description, IUserActionLogger logger)
        {
            Contract.Requires(!string.IsNullOrEmpty(name));
            Contract.Requires(logger != null);

            Logger = logger;

            Name = name;
            Description = description;
            Created = DateTime.Now;
            Updated = Created;
            State = ProjectState.InProgress;
            Tasks = new List<Task>();
            Logs = new List<ProjectLogEntry>();

            Logger.LogCreateProject(this);
            if (Log.IsDebugEnabled) Log.Debug("Created project.Name=[{0}]", name);
        }

        /// <summary>
        /// Finalizes the project
        /// </summary>
        public void Close()
        {
            Contract.Requires(State == ProjectState.InProgress);
            Contract.Ensures(State == ProjectState.Finished);

            Tuple<bool, string> validationResult = ProjectValidationRules.PermitClose(this);

            if (!validationResult.Item1)
            {
                throw new UserActionFailureException(validationResult.Item2);
            }

            State = ProjectState.Finished;

            Updated = DateTime.Now;

            Logger.LogCloseProject(this);

            if (Log.IsDebugEnabled) Log.Debug("Closed project.Name=[{0}]", Name);
        }

        /// <summary>
        /// Reopens the project
        /// </summary>
        public void Reopen()
        {
            Contract.Requires(State == ProjectState.Finished);
            Contract.Ensures(State == ProjectState.InProgress);

            State = ProjectState.InProgress;

            Logger.LogReopenProject(this);

            if (Log.IsDebugEnabled) Log.Debug("Reopened project.Name=[{0}]", Name);
        }

        /// <summary>
        /// Creates new task and adds it in list of project's tasks
        /// </summary>
        internal void AddTask(Task task)
        {
            Contract.Requires(State == ProjectState.InProgress);

            Updated = DateTime.Now;

            Tasks.Add(task);

            if (Log.IsDebugEnabled) Log.Debug("Added task.Summary=[{0}]", task.Summary);
        }

        /// <summary>
        /// Deletes a task
        /// </summary>
        /// <param name="task">Task</param>
        internal void DeleteTask(Task task)
        {
            Contract.Requires(State == ProjectState.InProgress);

            var project = task.Project;

            var taskObj = Tasks.Where(t => t.Id == task.Id).Single();
            Tasks.Remove(taskObj);

            Updated = DateTime.Now;

            Logger.LogDeleteTask(project, task.Summary);
            if (Log.IsDebugEnabled) Log.Debug("Deleted task.Summary=[{0}]", task.Summary);
        }

        /// <summary>
        /// Project identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Project name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Tasks
        /// </summary>
        /// <remarks>Lazy-loaded property</remarks>
        public virtual ICollection<Task> Tasks { get; private set; }

        /// <summary>
        /// Project log
        /// </summary>
        /// <remarks>Lazy-loaded property</remarks>
        public virtual ICollection<ProjectLogEntry> Logs { get; private set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Project state
        /// </summary>
        public ProjectState State { get; set; }

        /// <summary>
        /// Date and time of creation
        /// </summary>
        public DateTime Created { get; private set; }

        /// <summary>
        /// Last modification date and time
        /// </summary>
        public DateTime Updated { get; private set; }

        /// <summary>
        /// User action logger
        /// </summary>
        public IUserActionLogger Logger { get; set; }

    }
}
