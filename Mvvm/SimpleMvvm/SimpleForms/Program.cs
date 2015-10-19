using SimpleMvvm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SimpleForms
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			ModelContext.Default.CustomerRepository = new SimpleMvvm.Data.Naive.CustomerRepository();

			var form = new CustomerForm();
			Application.Run(form);
		}
	}
}
