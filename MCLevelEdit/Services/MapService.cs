using MCLevelEdit.Domain;

namespace MCLevelEdit.Services
{
    internal class MapService
    {
        public Map CreateMap(ushort width = 256, ushort height = 256)
        {
            var squares = new Square[width,height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    squares[x, y] = new Square(new Position(x, y), new Entity[] { });
                }
            }

            return new Map(squares);
        }
    }
}
