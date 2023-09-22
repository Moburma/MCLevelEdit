using MCLevelEdit.DataModel;

namespace MCLevelEdit.Services
{
    internal class MapService
    {
        public Map CreateMap(ushort size = 256)
        {
            var squares = new Square[size, size];

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    squares[x, y] = new Square(new Position(x, y), new Entity[] { });
                }
            }

            return new Map(squares, size);
        }
    }
}
