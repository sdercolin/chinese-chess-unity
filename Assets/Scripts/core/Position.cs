namespace Core
{
    public struct Position
    {
        public int X { get; }
        public int Y { get; }

        public Position(Color color, int relativeX, int relativeY)
        {
            if (color == Color.Black)
            {
                X = relativeX;
                Y = relativeY;
            }
            else
            {
                X = MAX_X - relativeX;
                Y = MAX_Y - relativeY;
            }
        }

        public const int MAX_X = 8;
        public const int MAX_Y = 9;

    }
}
