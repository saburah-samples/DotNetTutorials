using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Duv.WorkLog.Domain
{
	public class ProjectController
	{
		private HashSet<Project> projects;
		private long lastProjectId = 0;
		private long lastSprintId = 0;

		public ProjectController()
		{
			projects = new HashSet<Project>();
		}

		public Project CreateNewProject()
		{
			var result = new Project();
			return result;
		}

		public Project SaveProject(Project project)
		{
			var result = projects.FirstOrDefault(p => p.Id == project.Id || p.Name == project.Name);
			if (result == null)
			{
				result = new Project();
				result.Id = GetNextProjectId();
				result.Name = project.Name;
				result.Description = project.Description;
				result.CreatedBy = GetCurrentUser();
				result.Created = DateTime.Now;
				result.Modified = DateTime.Now;
				projects.Add(result);
			}
			else
			{
				result.Name = project.Name;
				result.Description = project.Description;
				result.Modified = DateTime.Now;
			}

			return CloneProject(result);
		}

		public Project GetProjectById(long id)
		{
			var project = projects.FirstOrDefault(p => p.Id == id);
			if (project == null)
				return null;

			return CloneProject(project);
		}

		public Sprint CreateNewSprint(long projectId)
		{
			var project = GetProjectById(projectId);
			Debug.Assert(project != null);

			var result = new Sprint();
			result.Project = project;
			return result;
		}

		public Sprint SaveSprint(Sprint sprint)
		{
			Debug.Assert(sprint != null);
			Debug.Assert(sprint.Project != null);

			var project = projects.FirstOrDefault(p => p.Id == sprint.Project.Id);
			Debug.Assert(project != null);

			var result = project.Sprints.FirstOrDefault(s => s.Id == sprint.Id || s.Name == sprint.Name);
			if (result == null)
			{
				result = new Sprint();
				result.Id = GetNextSprintId();
				result.Name = sprint.Name;
				result.Description = sprint.Description;
				result.CreatedBy = GetCurrentUser();
				result.Created = DateTime.Now;
				result.Modified = DateTime.Now;
				result.Project = project;
				project.Sprints.Add(result);
			}
			else
			{
				result.Name = sprint.Name;
				result.Description = sprint.Description;
				result.Modified = DateTime.Now;
			}

			return CloneSprint(result);
		}

		public Sprint GetSprintById(long id)
		{
			var project = projects.FirstOrDefault(p => p.Sprints.Any(s => s.Id == id));
			if (project == null)
				return null;
			var sprint = project.Sprints.FirstOrDefault(s => s.Id == id);
			if (sprint == null)
				return null;
			return CloneSprint(sprint);
		}

		public Task CreateNewTask(Project project)
		{
			Debug.Assert(project != null);

			var result = new Task();
			result.Project = project;
			project.Tasks.Add(result);
			return result;
		}

		public Task CreateNewTask(Sprint sprint)
		{
			Debug.Assert(sprint != null);

			var result = CreateNewTask(sprint.Project);
			result.Sprint = sprint;
			sprint.Tasks.Add(result);
			return result;
		}

		private Project CloneProject(Project project)
		{
			var result = new Project();
			result.Id = project.Id;
			result.Name = project.Name;
			result.Description = project.Description;
			result.CreatedBy = project.CreatedBy;
			result.Created = project.Created;
			result.Modified = project.Modified;

			foreach(var sprint in project.Sprints)
			{
				var item = CloneSprint(sprint, result);
				result.Sprints.Add(item);
			}
			return result;
		}

		private Sprint CloneSprint(Sprint sprint, Project project)
		{
			var result = new Sprint();
			result.Id = sprint.Id;
			result.Name = sprint.Name;
			result.Description = sprint.Description;
			result.CreatedBy = sprint.CreatedBy;
			result.Created = sprint.Created;
			result.Modified = sprint.Modified;
			result.Project = project;
			return result;
		}

		private Sprint CloneSprint(Sprint sprint)
		{
			var result = new Sprint();
			result.Id = sprint.Id;
			result.Name = sprint.Name;
			result.Description = sprint.Description;
			result.CreatedBy = sprint.CreatedBy;
			result.Created = sprint.Created;
			result.Modified = sprint.Modified;
			result.Project = (sprint.Project == null) ? null : CloneProject(sprint.Project);
			return result;
		}

		private string GetCurrentUser()
		{
			return "Current user";
		}

		private long GetNextProjectId()
		{
			lastProjectId++;
			return lastProjectId;
		}

		private long GetNextSprintId()
		{
			lastSprintId++;
			return lastSprintId;
		}
	}
}
