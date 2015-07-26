using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Duv.WorkLog.Domain;

namespace Duv.WorkLog.Tests
{
	[TestClass]
	public class TaskTests
	{
		private ProjectController controller;

		private static void AssertTaskInProject(Project project, Task result)
		{
			Assert.IsNotNull(result.Project);
			Assert.AreEqual(project, result.Project);
			var task = project.Tasks.FirstOrDefault(t => t.Equals(result));
			Assert.IsNotNull(task);
		}

		private static void AssertTaskInSprint(Sprint sprint, Task result)
		{
			Assert.IsNotNull(result.Sprint);
			Assert.AreEqual(sprint, result.Sprint);
			var task = sprint.Tasks.FirstOrDefault(t => t.Equals(result));
			Assert.AreEqual(result, task);
		}

		private static void AssertTaskNotInSprint(Project project, Task result)
		{
			Assert.IsNull(result.Sprint);
			var sprint = project.Sprints.FirstOrDefault(s => s.Tasks.Any(t => t.Equals(result)));
			Assert.IsNull(sprint);
		}

		[TestInitialize]
		public void Initialize()
		{
			controller = new ProjectController();
		}

		[TestMethod]
		public void TestCreateNewProjectTask()
		{
			var project = controller.CreateNewProject();
			project = controller.SaveProject(project);
			var result = controller.CreateNewTask(project);

			Assert.IsNotNull(result);
			Assert.AreNotEqual(DateTime.MinValue, result.Created);
			Assert.AreNotEqual(DateTime.MinValue, result.Modified);
			Assert.IsNotNull(result.Kind);
			Assert.IsNotNull(result.Status);
			Assert.IsNotNull(result.Activities);
			Assert.IsNotNull(result.Attachments);
			Assert.IsNotNull(result.Comments);

			AssertTaskInProject(project, result);
			AssertTaskNotInSprint(project, result);
		}

		[TestMethod]
		public void TestCreateNewSprintTask()
		{
			var project = controller.CreateNewProject();
			project = controller.SaveProject(project);
			var sprint = controller.CreateNewSprint(project.Id);
			var result = controller.CreateNewTask(sprint);

			Assert.IsNotNull(result);
			Assert.AreNotEqual(DateTime.MinValue, result.Created);
			Assert.AreNotEqual(DateTime.MinValue, result.Modified);
			Assert.IsNotNull(result.Kind);
			Assert.IsNotNull(result.Status);
			Assert.IsNotNull(result.Activities);
			Assert.IsNotNull(result.Attachments);
			Assert.IsNotNull(result.Comments);

			AssertTaskInProject(project, result);
			AssertTaskInSprint(sprint, result);
		}
	}
}
