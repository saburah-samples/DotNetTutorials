using Duv.Domain.Triggers.Models;
using System.Collections.Generic;
using System;

namespace Duv.UI.Triggers.ViewModels
{
	internal class MonitoringFileEditor : ModelEditorBase<MonitoringFile>
	{
		protected override bool HasChanges(MonitoringFile current)
		{
			throw new NotImplementedException();
		}

		protected override void OnAcceptChanges(MonitoringFile key, MonitoringFile value)
		{
			throw new NotImplementedException();
		}

		protected override void OnCancelChanges(MonitoringFile editedModel, MonitoringFile cleanModel)
		{
			throw new NotImplementedException();
		}

		protected override MonitoringFile OnGetCleanModel(MonitoringFile model)
		{
			throw new NotImplementedException();
		}
	}
}