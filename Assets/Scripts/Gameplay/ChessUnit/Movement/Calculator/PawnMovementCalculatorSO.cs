namespace DCG.Gameplay
{
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = "PawnMovementCalculator", menuName = "DCG/Chess Unit/Movement/Pawn Movement Calculator")]
    public class PawnMovementCalculatorSO : ChessUnitMovementCalculatorSO
    {
        public override IEnumerable<BoardCoordinate> CalculateMoveableCoordinates(BoardCoordinate origin, int mapSize, bool isFirstMove, bool isPlayerCoordinate)
        {
            int moveDelta = isFirstMove ? 2 : 1;

            BoardManager bm = BoardManager.Instance;

            for (int i = 0; i < moveTemplates.Length; i++) 
            {
                BoardCoordinate coordinate = new BoardCoordinate(origin.column, origin.row + moveTemplates[i].row * moveDelta);

                if (bm.IsUnitInCoordinate(isPlayerCoordinate, coordinate))
                    break;

                yield return coordinate;
            }
                

        }
    }
}