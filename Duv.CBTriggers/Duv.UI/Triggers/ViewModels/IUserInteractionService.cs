using System.Collections.Generic;

namespace Duv.UI.Triggers.ViewModels
{
	public interface IUserInteractionService
	{
		bool GetConfirmation(string message, string caption);

		void SendNotification(string message, string caption);

		bool GetFiles(string filter, bool multiselect, out IEnumerable<string> files);
	}
}