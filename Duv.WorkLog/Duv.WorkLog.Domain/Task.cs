using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Duv.WorkLog.Domain
{
	public class Task : EntryBase
	{
		public Task()
		{
			Created = DateTime.Now;
			Modified = DateTime.Now;
			Kind = new TaskKind();
			Status = new TaskStatus();
			this.Comments = new List<Comment>();
			this.Attachments = new List<Attachment>();
			this.Activities = new List<Activity>();
		}

		public TaskKind Kind { get; set; }
		public TaskStatus Status { get; set; }
		public Project Project { get; set; }
		public Sprint Sprint { get; set; }

		public ICollection<Comment> Comments { get; set; }
		public ICollection<Attachment> Attachments { get; set; }

		public ICollection<Activity> Activities { get; set; }
	}
}
