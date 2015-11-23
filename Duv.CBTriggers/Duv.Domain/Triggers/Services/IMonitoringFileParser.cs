using Duv.Domain.Triggers.Models;

namespace Duv.Domain.Triggers.Services
{
	public interface IMonitoringFileParser
	{
		MonitoringFile ParseAll(string fileName);
		MonitoringFile ParseInfo(string fileName);
		MonitoringFile ParseFile(MonitoringFile file);
    }
}