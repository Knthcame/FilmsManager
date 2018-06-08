using System.Threading.Tasks;
using Acr.UserDialogs;
using FilmsManager.Services.Interfaces;

namespace FilmsManager.iOS.Helpers
{
	public class NotificationHelper : INotificationHelper
		{
			public Task<bool> ShowDialog(string Title, string Message, string okText, string cancelText)
			{
				return UserDialogs.Instance.ConfirmAsync(Message, Title, okText, cancelText);
			}
		}
}