using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Duv.Domain.Triggers.Models;

namespace Duv.Domain.Triggers.Services
{
	public class FileDocumentParsedEventArgs : EventArgs
	{
		public FileDocumentParsedEventArgs(MonitoringFile file, MonitoringDocument document)
		{
			this.File = file;
			this.Document = document;
		}

		public MonitoringFile File { get; private set; }
		public MonitoringDocument Document { get; private set; }
	}

	public class MonitoringFileParser : IMonitoringFileParser
	{
		public event EventHandler<FileDocumentParsedEventArgs> FileDocumentParsed;

		public MonitoringFile ParseAll(string fileName)
		{
			var file = ParseInfo(fileName);
			return ParseFile(file);
		}

		public MonitoringFile ParseInfo(string fileName)
		{
			var result = new MonitoringFile();

			var info = new System.IO.FileInfo(fileName);
			result.Name = info.FullName;
			result.Size = info.Length;
			result.Modified = info.LastWriteTime;//.Trancate(TimeSpan.FromMilliseconds(100));

			return result;
		}

		public MonitoringFile ParseFile(MonitoringFile file)
		{
			var documents = new List<MonitoringDocument>();
			// TODO: parse file documents
			
			file.Documents = documents;
			return file;
		}

		private void OnFileDocumentParsed(MonitoringFile file, MonitoringDocument document)
		{
			if (FileDocumentParsed != null)
			{
				FileDocumentParsed(this, new FileDocumentParsedEventArgs(file, document));
			}
			document.FileId = 0;
			document.DocumentId = 0;
			document.TypeId = 0;
			document.PersonId = 0;
			document.CustomerId = 0;
		}
	}
}
