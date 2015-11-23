using System;
using System.Collections.Generic;
using System.Linq;

namespace Duv.UI.Triggers.ViewModels
{
	public abstract class ModelEditorBase<TModel> : IModelEditor<TModel>
		where TModel : class
	{
		protected readonly IList<TModel> models;
		protected readonly IDictionary<TModel, TModel> updatedModels;
		private TModel currentModel;

		public ModelEditorBase()
		{
			updatedModels = new Dictionary<TModel, TModel>();
		}

		public void Resume(TModel model)
		{
			CurrentModel = model;
			if (model == null) return;

			if (!updatedModels.ContainsKey(model))
			{
				var cleanModel = OnGetCleanModel(model);
				updatedModels.Add(model, cleanModel);
			}
		}

		public void Suspend(TModel model)
		{
			if (model == null) return;

			if (updatedModels.ContainsKey(model))
			{
				if (HasChanges(model)) return;
				updatedModels.Remove(model);
			}
		}

		public bool HasChanges()
		{
			return HasChanges(CurrentModel) || updatedModels.Count > 1 && !updatedModels.ContainsKey(CurrentModel);
		}

		public void AcceptChanges()
		{
			foreach (var item in updatedModels)
			{
				OnAcceptChanges(item.Key, item.Value);
			}
			updatedModels.Clear();
		}


		public void CancelChanges()
		{
			foreach (var item in updatedModels.Where(e => e.Value != null))
			{
				OnCancelChanges(item.Key, item.Value);
			}
			updatedModels.Clear();
		}

		protected abstract void OnAcceptChanges(TModel key, TModel value);

		protected abstract void OnCancelChanges(TModel editedModel, TModel cleanModel);

		protected abstract TModel OnGetCleanModel(TModel model);

		protected abstract bool HasChanges(TModel current);

		public TModel CurrentModel
		{
			get { return currentModel; }
			set
			{
				Suspend(currentModel);
				currentModel = value;
				Resume(currentModel);
			}
		}
	}
}