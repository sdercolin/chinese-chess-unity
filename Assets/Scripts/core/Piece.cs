using System.Collections.Generic;
using System.Linq;
using System;

namespace Core
{
    public abstract class Piece
    {
        public Piece(Color color, Position position)
        {
            Color = color;
            Position = position;
        }

        public Piece(Color color, int relativeX, int relativeY)
        {
            Color = color;
            Position = new Position(color, relativeX, relativeY);
        }

        public Color Color { get; }

        public abstract PieceType Type { get; }

        public Position Position { get; protected set; }

        public abstract List<Position> GetMovablePositions(Game game);

        protected int UpwardFactor => Color == Color.Red ? 1 : -1;

        protected bool IsSameColor(Piece piece)
        {
            if (piece == null)
            {
                return false;
            }
            return piece.Color == Color;
        }

        public void MoveTo(Position position)
        {
            Position = position;
        }
    }

    public class General : Piece
    {
        public override PieceType Type => PieceType.General;

        public General(Color color, int relativeX, int relativeY) : base(color, relativeX, relativeY)
        {
        }

        public override List<Position> GetMovablePositions(Game game)
        {
            return Position.Palaces[Color]
                .Where(pos => Position.Distance(pos, Position) == 1)
                .Where(pos => !IsSameColor(game.GetPieceAt(pos)))
                .ToList();
        }
    }

    public class Advisor : Piece
    {
        public override PieceType Type => PieceType.Advisor;

        public Advisor(Color color, int relativeX, int relativeY) : base(color, relativeX, relativeY)
        {
        }

        public override List<Position> GetMovablePositions(Game game)
        {
            return Position.Palaces[Color]
                .Where(pos => Position.Distance(pos, Position) == 2 && pos.X != Position.X && pos.Y != Position.Y)
                .Where(pos => !IsSameColor(game.GetPieceAt(pos)))
                .ToList();
        }
    }

    public class Elephant : Piece
    {
        public override PieceType Type => PieceType.Elephant;

        public Elephant(Color color, int relativeX, int relativeY) : base(color, relativeX, relativeY)
        {
        }

        public override List<Position> GetMovablePositions(Game game)
        {
            return Position.Halves[Color]
                .Where(pos => Position.Distance(pos, Position) == 4 && pos.X != Position.X && pos.Y != Position.Y)
                .Where(pos =>
                {
                    var route = Position.Route(Position, pos);
                    if (route.Count != 1)
                    {
                        return false;
                    }
                    return game.GetPieceAt(route[0]) == null;
                })
                .Where(pos => !IsSameColor(game.GetPieceAt(pos)))
                .ToList();
        }
    }

    public class Chariot : Piece
    {
        public override PieceType Type => PieceType.Chariot;

        public Chariot(Color color, int relativeX, int relativeY) : base(color, relativeX, relativeY)
        {
        }

        public override List<Position> GetMovablePositions(Game game)
        {
            return Position.Whole
                .Where(pos => pos.X == Position.X || pos.Y == Position.Y)
                .Where(pos => Position.Route(Position, pos).All(routePos => game.GetPieceAt(routePos) == null))
                .Where(pos => !IsSameColor(game.GetPieceAt(pos)))
                .ToList();
        }
    }

    public class Horse : Piece
    {
        public override PieceType Type => PieceType.Horse;

        public Horse(Color color, int relativeX, int relativeY) : base(color, relativeX, relativeY)
        {
        }

        public override List<Position> GetMovablePositions(Game game)
        {
            return Position.Whole
                .Where(pos => Position.Distance(pos, Position) == 1)
                .Where(pos => game.GetPieceAt(pos) == null)
                .SelectMany(middlePos =>
                {
                    int diffX = middlePos.X - Position.X;
                    int diffY = middlePos.Y - Position.Y;
                    if (diffX != 0)
                    {
                        return new List<Position>
                        {
                            new Position(middlePos.X + diffX, Position.Y - 1),
                            new Position(middlePos.X + diffX, Position.Y + 1)
                        };
                    }
                    else
                    {
                        return new List<Position>
                        {
                            new Position(Position.X - 1, middlePos.Y + diffY),
                            new Position(Position.X + 1, middlePos.Y + diffY)
                        };
                    }
                })
                .Where(pos => Position.IsOnTable(pos))
                .Where(pos => !IsSameColor(game.GetPieceAt(pos)))
                .ToList();
        }
    }

    public class Cannon : Piece
    {
        public override PieceType Type => PieceType.Cannon;

        public Cannon(Color color, int relativeX, int relativeY) : base(color, relativeX, relativeY)
        {
        }

        public override List<Position> GetMovablePositions(Game game)
        {
            var list = new List<Position>();
            list.AddRange(
                Position.Whole
                    .Where(pos => pos.X == Position.X || pos.Y == Position.Y)
                    .Where(pos => Position.Route(Position, pos).All(routePos => game.GetPieceAt(routePos) == null))
                    .Where(pos => game.GetPieceAt(pos) == null)
                    .ToList()
            );
            list.AddRange(
                Position.Whole
                    .Where(pos => pos.X == Position.X || pos.Y == Position.Y)
                    .Where(pos => game.GetPieceAt(pos) != null && !IsSameColor(game.GetPieceAt(pos)))
                    .Where(pos => Position.Route(Position, pos).Count(routePos => game.GetPieceAt(routePos) != null) == 1)
                    .ToList()
            );
            return list;
        }
    }

    public class Soldier : Piece
    {
        public override PieceType Type => PieceType.Soldier;

        public Soldier(Color color, int relativeX, int relativeY) : base(color, relativeX, relativeY)
        {
        }

        public override List<Position> GetMovablePositions(Game game)
        {
            var list = new List<Position>();
            list.AddRange(
                new List<Position> { Position.move(0, 1 * UpwardFactor) }.Where(pos => !IsSameColor(game.GetPieceAt(pos)))
            );
            if (!Position.Halves[Color].Contains(Position))
            {
                list.AddRange(
                    new List<Position> { Position.move(-1, 0), Position.move(1, 0) }.Where(pos => !IsSameColor(game.GetPieceAt(pos)))
                );
            }
            return list;
        }
    }

    public enum PieceType
    {
        General,
        Advisor,
        Elephant,
        Chariot,
        Horse,
        Cannon,
        Soldier
    }

    public enum Color
    {
        Red,
        Black
    }

}
