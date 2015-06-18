using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Duv.TaskManager.Domain.Model
{
    public class UserActionFailureException : Exception
    {
        private string p;

        public UserActionFailureException(string p)
        {
            // TODO: Complete member initialization
            this.p = p;
        }
    }
}
