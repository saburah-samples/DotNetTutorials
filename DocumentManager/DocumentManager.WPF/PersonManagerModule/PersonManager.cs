using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManager.WPF.PersonManagerModule
{
    public class PersonManager
    {
        public PersonManager()
        {
            Title = "Person manager";
        }
        public string Title { get; set; }
        public IReadOnlyCollection<Person> Persons { get; set; }
    }
}
