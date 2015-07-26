using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Duv.WorkLog.Domain
{
	public class Sprint : EntryBase
	{
		public Sprint()
		{
			this.Tasks = new List<Task>();
			this.Comments = new List<Comment>();
			this.Attachments = new List<Attachment>();
			this.Activities = new List<Activity>();
		}

		public long Id { get; set; }

		public Project Project { get; set; }
		public ICollection<Task> Tasks { get; set; }

		public ICollection<Comment> Comments { get; set; }
		public ICollection<Attachment> Attachments { get; set; }

		public ICollection<Activity> Activities { get; set; }
	}
}
