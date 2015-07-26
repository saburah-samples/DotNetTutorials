using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Duv.WorkLog.Domain
{
	public class Activity
	{
		public const string ProjectCreated = "Project has been created.";
		public const string SprintCreated = "Sprint has been created.";
		public const string TaskCreated = "Task has been created.";

		public string Name { get; set; }
		public string CreatedBy { get; set; }
		public DateTime Created { get; set; }
		public EntryBase Source { get; set; }
	}
}
