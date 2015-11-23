namespace Duv.UI.Triggers.ViewModels
{
	public interface ICommand
	{
		void Execute();
		bool CanExecute();
	}
}