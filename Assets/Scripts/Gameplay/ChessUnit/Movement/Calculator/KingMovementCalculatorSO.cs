namespace DCG.Gameplay
{
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = "KingMovementCalculator", menuName = "DCG/Chess Unit/Movement/King Movement Calculator")]
    public class KingMovementCalculatorSO : ChessUnitMovementCalculatorSO
    {
        public override IEnumerable<BoardCoordinate> CalculateMoveableCoordinates(BoardCoordinate origin, int mapSize, bool isFirstMove, bool isPlayerCoordinate)
        {
            BoardCoordinate coordinate = new BoardCoordinate();

            BoardManager bm = BoardManager.Instance;

            for (int i = 0; i < moveTemplates.Length; i++)
            {
                coordinate.column = origin.column + moveTemplates[i].column;
                coordinate.row = origin.row + moveTemplates[i].row;

                if(bm.IsUnitInCoordinate(isPlayerCoordinate, coordinate))
                    continue;

                if (bm.IsCoordinateOtherUnitMoveCoordinate(!isPlayerCoordinate, coordinate))
                    continue;

                yield return coordinate;
            }
        }
    }
}
