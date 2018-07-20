using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Models.Classes
{
	public class BaseModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
		{
			if (string.IsNullOrEmpty(propertyName))
				return;
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}