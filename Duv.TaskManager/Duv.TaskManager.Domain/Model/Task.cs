using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Duv.TaskManager.Domain.Model
{
    public class Task
    {
        public int Id { get; set; }
        public Project Project { get; set; }

        public string Summary { get; set; }
    }
}
