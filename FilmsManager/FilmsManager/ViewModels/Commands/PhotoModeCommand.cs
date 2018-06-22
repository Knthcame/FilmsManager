using FilmsManager.Models;
using Plugin.Media;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FilmsManager.ViewModels.Commands
{
	class PhotoModeCommand : ICommand
	{
		public event EventHandler CanExecuteChanged;

		private PickImageViewModel _viewModel;

		public PhotoModeCommand(PickImageViewModel viewModel)
		{
			_viewModel = viewModel;
		}

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public async void Execute(object parameter)
		{
			switch (parameter)
			{
				case "Preloaded":
					_viewModel.ListViewVisible = true;
					_viewModel.ButtonsVisible = false;
					break;

				case "Gallery":
					await PickGalleryPhotoAsync();
					break;
			}
		}

		private async Task PickGalleryPhotoAsync()
		{
			var photo = await CrossMedia.Current.PickPhotoAsync();
			PickImageModel model = new PickImageModel
			{
				ImageName = FileImageSource.FromStream(() =>
				{
					var stream = photo.GetStream();
					return stream;
				})
			};
			_viewModel.PickImageCommand.Execute(model);
		}
	}
}
