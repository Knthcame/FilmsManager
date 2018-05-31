using Acr.UserDialogs;
using Android.App;
using FilmsManager.Services.Interfaces;
using System.Threading.Tasks;

namespace FilmsManager.Droid.Helpers
{
	public class NotificationHelper : INotificationHelper
	{
		public Task<bool> ShowDialog(string Title, string Message, string okText, string cancelText)
		{
			return UserDialogs.Instance.ConfirmAsync(Message, Title, okText, cancelText);
		}
	}
}