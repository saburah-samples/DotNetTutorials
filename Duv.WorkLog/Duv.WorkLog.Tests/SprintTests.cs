using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Duv.WorkLog.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Duv.WorkLog.Tests
{
	[TestClass]
	public class SprintTests
	{
		private ProjectController controller;

		[TestInitialize]
		public void Initialize()
		{
			controller = new ProjectController();
		}

		[TestMethod]
		public void TestCreateNewSprint()
		{
			var project = controller.CreateNewProject();
			project.Name = "Test project name";
			project.Description = "Test project description";
			project = controller.SaveProject(project);

			var result = controller.CreateNewSprint(project.Id);

			Assert.IsNotNull(result);
			Assert.AreEqual(0, result.Id);
			Assert.IsNull(result.Name);
			Assert.IsNull(result.Description);
			Assert.IsNull(result.CreatedBy);
			Assert.AreEqual(DateTime.MinValue, result.Created);
			Assert.IsNull(result.ModifiedBy);
			Assert.AreEqual(DateTime.MinValue, result.Modified);

			Assert.IsNotNull(result.Activities);
			Assert.IsNotNull(result.Attachments);
			Assert.IsNotNull(result.Comments);
			Assert.IsNotNull(result.Tasks);

			Assert.IsNotNull(result.Project);
			Assert.AreEqual(project.Id, result.Project.Id);
		}

		[TestMethod]
		public void TestSaveNewSprint()
		{
			var project = controller.CreateNewProject();
			project.Name = "Test project name";
			project.Description = "Test project description";
			project = controller.SaveProject(project);

			var sprint = controller.CreateNewSprint(project.Id);
			sprint.Name = "Test sprint name";
			sprint.Description = "Test sprint description";

			var result = controller.SaveSprint(sprint);

			Assert.IsNotNull(result);
			Assert.AreNotSame(result, sprint);
			Assert.AreNotEqual(0, result.Id);
			Assert.AreEqual(sprint.Name, result.Name);
			Assert.AreEqual(sprint.Description, result.Description);
			Assert.IsNotNull(result.CreatedBy);
			Assert.AreNotEqual(DateTime.MinValue, result.Created);
			Assert.IsNotNull(result.ModifiedBy);
			Assert.AreNotEqual(DateTime.MinValue, result.Modified);

			Assert.IsNotNull(result.Activities);
			Assert.IsNotNull(result.Attachments);
			Assert.IsNotNull(result.Comments);
			Assert.IsNotNull(result.Tasks);

			Assert.IsNotNull(result.Project);
			Assert.AreEqual(project.Id, result.Project.Id);

			project = controller.GetProjectById(project.Id);
			result = project.Sprints.FirstOrDefault(s => s.Id == result.Id);
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void TestGetSprintById()
		{
			var project = controller.CreateNewProject();
			project.Name = "Test project name";
			project.Description = "Test project description";
			project = controller.SaveProject(project);
			var sprint = controller.CreateNewSprint(project.Id);
			sprint.Name = "Test sprint name";
			sprint.Description = "Test sprint description";
			sprint = controller.SaveSprint(sprint);

			Sprint result = controller.GetSprintById(sprint.Id);

			Assert.IsNotNull(result);
			Assert.AreNotSame(result, sprint);
			Assert.AreEqual(sprint.Id, result.Id);
			Assert.AreEqual(sprint.Name, result.Name);
			Assert.AreEqual(sprint.Description, result.Description);
			Assert.AreEqual(sprint.CreatedBy, result.CreatedBy);
			Assert.AreEqual(sprint.Created, result.Created);
			Assert.AreEqual(sprint.ModifiedBy, result.ModifiedBy);
			Assert.AreEqual(sprint.Modified, result.Modified);

			Assert.IsNotNull(result.Activities);
			Assert.IsNotNull(result.Attachments);
			Assert.IsNotNull(result.Comments);
			Assert.IsNotNull(result.Tasks);
		}
	}
}
