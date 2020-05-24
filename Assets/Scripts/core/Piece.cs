namespace Core
{
    public abstract class Piece
    {
        public Color Color { get; }
        public abstract PieceType Type { get; }

        public Position Position { get; protected set; }

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
    }

    public class General : Piece
    {
        public override PieceType Type => PieceType.General;

        public General(Color color, int relativeX, int relativeY) : base(color, relativeX, relativeY)
        {
        }
    }

    public class Advisor : Piece
    {
        public override PieceType Type => PieceType.Advisor;

        public Advisor(Color color, int relativeX, int relativeY) : base(color, relativeX, relativeY)
        {
        }
    }

    public class Elephant : Piece
    {
        public override PieceType Type => PieceType.Elephant;

        public Elephant(Color color, int relativeX, int relativeY) : base(color, relativeX, relativeY)
        {
        }
    }

    public class Chariot : Piece
    {
        public override PieceType Type => PieceType.Chariot;

        public Chariot(Color color, int relativeX, int relativeY) : base(color, relativeX, relativeY)
        {
        }
    }

    public class Horse : Piece
    {
        public override PieceType Type => PieceType.Horse;

        public Horse(Color color, int relativeX, int relativeY) : base(color, relativeX, relativeY)
        {
        }
    }

    public class Cannon : Piece
    {
        public override PieceType Type => PieceType.Cannon;

        public Cannon(Color color, int relativeX, int relativeY) : base(color, relativeX, relativeY)
        {
        }
    }

    public class Soldier : Piece
    {
        public override PieceType Type => PieceType.Soldier;

        public Soldier(Color color, int relativeX, int relativeY) : base(color, relativeX, relativeY)
        {
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
