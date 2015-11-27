using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Duv.Domain.Triggers.Models;
using Duv.Domain.Triggers.Repositories;
using Duv.Domain.Triggers.Services;

namespace Duv.UI.Triggers.ViewModels
{
	public class MonitoringFilePageViewModel : ViewModelBase
	{
		private readonly IMonitoringFileService fileService;
		private readonly IMonitoringFileParser fileParser;
		private readonly IModelEditor<MonitoringFile> fileEditor;

		public MonitoringFilePageViewModel()
		{
			var locator = new RepositoryLocatorInMemory(); // TODO: sync locator with DB
			this.fileService = new MonitoringFileService(locator); //inject locator
			this.fileParser = new MonitoringFileParser();//?? inject customer service
			this.fileEditor = new MonitoringFileEditor();
		}

		public void Refresh()
		{
			fileEditor.Suspend(CurrentFile);
			try
			{
				fileEditor.CancelChanges();
				Files = fileService.FindAllFiles();
			}
			finally
			{
				fileEditor.Resume(CurrentFile);
			}
			OnPropertyChanged("Files");
		}

		public void UploadFiles()
		{
			IEnumerable<string> files;
			var isConfirmed = UserInteraction.GetFiles("CSV (Comma delimited) (*.csv)|*.csv", true, out files);
			if (!isConfirmed) return;

			if (!files.Any())
			{
				UserInteraction.SendNotification("No files selected to upload.", "Information");
				return;
			}

			fileEditor.Suspend(CurrentFile);
			try
			{
				foreach (var fileName in files)
				{
					var parseFile = fileParser.ParseInfo(fileName);
					var foundFile = fileService.GetFileByPath(parseFile.Name);
					if (foundFile == null)
					{
						parseFile = fileParser.ParseFile(parseFile);
						parseFile = fileService.CreateFile(parseFile);
						Files.Add(parseFile);
					}
					else if (parseFile.Size != foundFile.Size || parseFile.Modified != foundFile.Modified)
					{
						parseFile = fileParser.ParseFile(parseFile);
						parseFile = fileService.UploadFile(parseFile);
					}
				}
			}
			finally
			{
				fileEditor.Resume(CurrentFile);
			}			
			OnPropertyChanged("Files");
		}

		public void DeleteFiles()
		{
			fileEditor.Suspend(CurrentFile);
			try
			{
				var files = this.SelectedFiles.ToArray();

				if (!files.Any())
				{
					UserInteraction.SendNotification("No files selected to delete.", "Information");
					return;
				}

				var isConfirmed = UserInteraction.GetConfirmation("Are you sure you want to delete selected files?", "Confirmation");
				if (!isConfirmed) return;

				foreach (var f in files)
				{
					fileService.DeleteFile(f.Id);
					Files.Remove(f);
				}
			}
			finally
			{
				fileEditor.Resume(CurrentFile);
			}
			OnPropertyChanged("Files");
		}

		public IList<MonitoringFile> Files { get; private set; }
		public MonitoringFile CurrentFile
		{
			get { return fileEditor.CurrentModel; }
			set
			{
				if (fileEditor.CurrentModel != value)
				{
                    fileEditor.Suspend(fileEditor.CurrentModel);
                    fileEditor.Resume(value);

					var file = fileEditor.CurrentModel;
					if (file != null && file.Documents == null)
					{
						file.Documents = fileService.FindDocuments(file.Id);
					}
					FileDocuments = (file != null) ? file.Documents : new List<MonitoringDocument>();
				}
			}
		}

		public IList<MonitoringFile> SelectedFiles { get; set; }
		public IList<MonitoringDocument> FileDocuments { get; private set; }

		public bool HasChanges
		{
			get { return fileEditor.HasChanges(); }
		}

		public ICommand RefreshCommand { get; private set; }
		public ICommand SaveCommand { get; private set; }
		public ICommand UploadFilesCommand { get; private set; }
		public ICommand DeleteFilesCommand { get; private set; }
		public ICommand SendStartMonitoringRequestCommand { get; private set; }
		public ICommand SendStopMonitoringRequestCommand { get; private set; }
	}
}
