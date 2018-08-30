using FilmsManager.Models;
using Models.Resources;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FilmsManager.Resources
{
    public static class PickImageGallery
    {
        public static IList<PickImageModel> DefaultGalleryImages { get; } = new ObservableCollection<PickImageModel>()
        {
            new PickImageModel()
            {
            ImageName = AppImages.Shrek

            },
            new PickImageModel()
            {
            ImageName = AppImages.Shrek2

            },
            new PickImageModel()
            {
            ImageName = AppImages.Shrek3

            },
            new PickImageModel()
            {
            ImageName = AppImages.InfinityWar

            },
            new PickImageModel()
            {
            ImageName = AppImages.HarryPotter

            },
            new PickImageModel()
            {
            ImageName = AppImages.LOTR

            }
        };
    }
}
