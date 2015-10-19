﻿using SimpleMvvm.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace SimpleWpf
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			ModelContext.Default.CustomerRepository = new SimpleMvvm.Data.Naive.CustomerRepository();
			base.OnStartup(e);
		}
	}
}
