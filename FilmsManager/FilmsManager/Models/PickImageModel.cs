namespace FilmsManager.Models
{
    public class PickImageModel : BaseModel
    {
        private string _image;

        public string Image
        {
            get => _image;
            set
            {
                _image = value;
                RaisePropertyChanged();
            }
        }
    }
}
