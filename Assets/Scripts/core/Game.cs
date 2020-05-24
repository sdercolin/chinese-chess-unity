using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class Game
    {
        public readonly List<Piece> Pieces = new List<Piece>();

        public void Reset()
        {
            Pieces.Clear();
            var initialPieces =
                (new List<Core.Color> { Color.Red, Color.Black })
                .SelectMany(color =>
                    new List<Piece>(){
                        new General(color, 4, 0),
                        new Advisor(color, 3, 0),
                        new Advisor(color, 5, 0),
                        new Elephant(color, 2, 0),
                        new Elephant(color, 6, 0),
                        new Horse(color, 1, 0),
                        new Horse(color, 7, 0),
                        new Chariot(color, 0, 0),
                        new Chariot(color, 8, 0),
                        new Cannon(color, 1, 2),
                        new Cannon(color, 7, 2),
                        new Soldier(color, 0, 3),
                        new Soldier(color, 2, 3),
                        new Soldier(color, 4, 3),
                        new Soldier(color, 6, 3),
                        new Soldier(color, 8, 3)
                    }
                );
            Pieces.AddRange(initialPieces);
        }
    }
}
