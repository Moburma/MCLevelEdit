using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MCLevelEdit.DataModel
{
    public class ObservableImage : ObservableObject
    {
        private WriteableBitmap _image;

        public WriteableBitmap Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }
    }
}
