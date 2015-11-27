using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Duv.UI.Triggers.ViewModels
{
	interface IModelEditor<TModel> where TModel : class
	{
		void Resume(TModel model);
		void Suspend(TModel model);

		bool HasChanges();
		void AcceptChanges();
		void CancelChanges();

		TModel CurrentModel { get; }
	}
}
