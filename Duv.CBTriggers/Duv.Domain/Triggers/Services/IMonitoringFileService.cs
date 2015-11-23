using Duv.Domain.Triggers.Models;
using System.Collections.Generic;

namespace Duv.Domain.Triggers.Services
{
	public interface IMonitoringFileService
	{
		IList<MonitoringFile> FindAllFiles();

		MonitoringFile GetFileByPath(string fileName);

		MonitoringFile CreateFile(MonitoringFile file);
		MonitoringFile UpdateFile(MonitoringFile file);
		MonitoringFile UploadFile(MonitoringFile file);
		void DeleteFile(long fileId);

		IList<MonitoringDocument> FindDocuments(long fileId);
	}
}