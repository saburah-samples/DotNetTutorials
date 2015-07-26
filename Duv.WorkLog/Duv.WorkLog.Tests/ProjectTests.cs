using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Duv.WorkLog.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Duv.WorkLog.Tests
{
	[TestClass]
	public class ProjectTests
	{
		ProjectController controller;

		[TestInitialize]
		public void Initialize()
		{
			controller = new ProjectController();
		}

		[TestMethod]
		public void TestCreateNewProject()
		{
			var result = controller.CreateNewProject();

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
			Assert.IsNotNull(result.Sprints);
			Assert.IsNotNull(result.Tasks);
		}

		[TestMethod]
		public void TestSaveNewProject()
		{
			var project = controller.CreateNewProject();
			project.Name = "Test project name";
			project.Description = "Test project description";

			var result = controller.SaveProject(project);

			Assert.IsNotNull(result);
			Assert.AreNotSame(result, project);
			Assert.AreNotEqual(0, result.Id);
			Assert.AreEqual(project.Name, result.Name);
			Assert.AreEqual(project.Description, result.Description);
			Assert.IsNotNull(result.CreatedBy);
			Assert.AreNotEqual(DateTime.MinValue, result.Created);
			Assert.IsNotNull(result.ModifiedBy);
			Assert.AreNotEqual(DateTime.MinValue, result.Modified);
			Assert.IsNotNull(result.Activities);
			Assert.IsNotNull(result.Attachments);
			Assert.IsNotNull(result.Comments);
			Assert.IsNotNull(result.Sprints);
			Assert.IsNotNull(result.Tasks);
		}

		[TestMethod]
		public void TestSaveExistProject()
		{
			var project = controller.CreateNewProject();
			project.Name = "Test project name";
			project.Description = "Test project description";
			project = controller.SaveProject(project);
			System.Threading.Thread.Sleep(1);

			var result = controller.GetProjectById(project.Id);
			result.Name = "Test project name 2";
			result.Description = "Test project description 2";
			result = controller.SaveProject(result);

			Assert.IsNotNull(result);
			Assert.AreNotSame(result, project);
			Assert.AreEqual(project.Id, result.Id);
			Assert.AreNotEqual(project.Name, result.Name);
			Assert.AreNotEqual(project.Description, result.Description);
			Assert.AreEqual(project.CreatedBy, result.CreatedBy);
			Assert.AreEqual(project.Created, result.Created);
			Assert.AreEqual(project.ModifiedBy, result.ModifiedBy);
			Assert.AreNotEqual(project.Modified, result.Modified);
			Assert.IsNotNull(result.Activities);
			Assert.IsNotNull(result.Attachments);
			Assert.IsNotNull(result.Comments);
			Assert.IsNotNull(result.Sprints);
			Assert.IsNotNull(result.Tasks);
		}

		[TestMethod]
		public void TestGetProjectById()
		{
			var project = controller.CreateNewProject();
			project.Name = "Test project name";
			project.Description = "Test project description";
			project = controller.SaveProject(project);

			var result = controller.GetProjectById(project.Id);

			Assert.IsNotNull(result);
			Assert.AreNotSame(result, project);
			Assert.AreEqual(project.Id, result.Id);
			Assert.AreEqual(project.Name, result.Name);
			Assert.AreEqual(project.Description, result.Description);
			Assert.AreEqual(project.CreatedBy, result.CreatedBy);
			Assert.AreEqual(project.Created, result.Created);
			Assert.AreEqual(project.ModifiedBy, result.ModifiedBy);
			Assert.AreEqual(project.Modified, result.Modified);
			Assert.IsNotNull(result.Activities);
			Assert.IsNotNull(result.Attachments);
			Assert.IsNotNull(result.Comments);
			Assert.IsNotNull(result.Sprints);
			Assert.IsNotNull(result.Tasks);
		}
	}
}
