using CommunityToolkit.Mvvm.ComponentModel;

namespace MCLevelEdit.DataModel
{
    public class Map : ObservableObject
    {
        public static int Size = 256;

        private Square[,] _squares;

        public Square[,] Squares
        {
            get { return _squares; }
            set
            {
                SetProperty(ref _squares, value);
            }
        }

        public Map(Square[,] squares)
        {
            _squares = squares;
        }
    }
}
