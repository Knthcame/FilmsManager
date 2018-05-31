using System.Threading.Tasks;

namespace FilmsManager.Services.Interfaces
{
	public interface INotificationHelper
	{
		Task<bool> ShowDialog(string Title, string Message, string okText, string cancelText);
	}
}
