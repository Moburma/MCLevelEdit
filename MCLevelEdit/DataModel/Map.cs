using CommunityToolkit.Mvvm.ComponentModel;

namespace MCLevelEdit.DataModel
{
    public class Map : ObservableObject
    {
        private Square[,] _squares;

        public static Map Instance { get; set; }

        public Square[,] Squares
        {
            get { return _squares; }
            set
            {
                SetProperty(ref _squares, value);
            }
        }

        public Map(Square[, ] squares)
        {
            _squares = squares;
        }
    }
}
