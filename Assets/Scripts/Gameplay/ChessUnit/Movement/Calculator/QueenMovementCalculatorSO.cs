namespace DCG.Gameplay
{
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = "QueenMovementCalculator", menuName = "DCG/Chess Unit/Movement/Queen Movement Calculator")]
    public class QueenMovementCalculatorSO : ChessUnitMovementCalculatorSO
    {
        public override IEnumerable<BoardCoordinate> CalculateMoveableCoordinates(BoardCoordinate origin, int mapSize, bool isFirstMove, bool isPlayerCoordinate)
        {
            BoardCoordinate coordinate = new BoardCoordinate();

            BoardManager bm = BoardManager.Instance;

            for (int i = 0; i < moveTemplates.Length; i++)
            {
                for (int j = 1; j <= mapSize; j++)
                {
                    coordinate.column = origin.column + moveTemplates[i].column * j;
                    coordinate.row = origin.row + moveTemplates[i].row * j;

                    if (bm.IsUnitInCoordinate(isPlayerCoordinate, coordinate))
                        break;

                    if (bm.IsUnitInCoordinate(!isPlayerCoordinate, coordinate))
                    {
                        yield return coordinate;
                        break;
                    }

                    yield return coordinate;
                }
            }
        }
    }
}