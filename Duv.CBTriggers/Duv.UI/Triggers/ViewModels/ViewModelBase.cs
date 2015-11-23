using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Duv.UI.Triggers.ViewModels
{
	public class ViewModelBase
	{
		protected void OnPropertyChanged(string propertyName)
		{
			throw new NotImplementedException();
		}

		public IUserInteractionService UserInteraction { get; private set; }
	}
}
