using System.Collections.Generic;
using System;

namespace Core
{
    public struct Position
    {
        public int X { get; }
        public int Y { get; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

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

        public Position move(int x, int y)
        {
            return new Position(this.X + x, this.Y + y);
        }

        public static int Distance(Position a, Position b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }

        public static bool IsOnTable(Position position)
        {
            return position.X >= 0 && position.X <= MAX_X && position.Y >= 0 && position.Y <= MAX_Y;
        }

        public static List<Position> Route(Position a, Position b)
        {
            var list = new List<Position>();
            if (a.X == b.X)
            {
                int minY = Math.Min(a.Y, b.Y);
                int maxY = Math.Max(a.Y, b.Y);
                for (var y = minY + 1; y < maxY; y++)
                {
                    list.Add(new Position(a.X, y));
                }
            }
            else if (a.Y == b.Y)
            {
                int minX = Math.Min(a.X, b.X);
                int maxX = Math.Max(a.X, b.X);
                for (var x = minX + 1; x < maxX; x++)
                {
                    list.Add(new Position(x, a.Y));
                }
            }
            else if (Math.Abs(a.X - b.X) == Math.Abs(a.Y - b.Y))
            {
                int minX = Math.Min(a.X, b.X);
                int maxX = Math.Max(a.X, b.X);
                int minY = Math.Min(a.Y, b.Y);
                int yFactor = (a.X - b.X) * (a.Y - b.Y) > 0 ? 1 : -1;
                for (var diff = 1; diff < maxX - minX; diff++)
                {
                    list.Add(new Position(minX + diff, minY + diff * yFactor));
                }
            }
            return list;
        }

        public static List<Position> Whole = CreateWholeTable();

        static List<Position> CreateWholeTable()
        {
            var list = new List<Position>();
            for (int x = 0; x < Position.MAX_X; x++)
            {
                for (int y = 0; y < Position.MAX_Y; y++)
                {
                    list.Add(new Position(x, y));
                }
            }
            return list;
        }

        public static Dictionary<Color, List<Position>> Halves = CreateHalfTables();

        static Dictionary<Color, List<Position>> CreateHalfTables()
        {
            var dictionary = new Dictionary<Color, List<Position>>();
            foreach (var color in new Color[] { Color.Red, Color.Black })
            {
                var list = new List<Position>();
                for (int x = 0; x < MAX_X; x++)
                {
                    for (int y = 0; y < 5; y++)
                    {
                        list.Add(new Position(color, x, y));
                    }
                }
                dictionary[color] = list;
            }
            return dictionary;
        }

        public static Dictionary<Color, List<Position>> Palaces = CreatePalaces();

        static Dictionary<Color, List<Position>> CreatePalaces()
        {
            var dictionary = new Dictionary<Color, List<Position>>();
            foreach (var color in new Color[] { Color.Red, Color.Black })
            {
                var list = new List<Position>();
                for (int x = 3; x < 6; x++)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        list.Add(new Position(color, x, y));
                    }
                }
                dictionary[color] = list;
            }
            return dictionary;
        }

        public const int MAX_X = 8;
        public const int MAX_Y = 9;

    }
}
