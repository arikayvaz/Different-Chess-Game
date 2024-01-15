namespace DCG.Gameplay
{
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = "KnightMovementCalculator", menuName = "DCG/Chess Unit/Movement/Knight Movement Calculator")]
    public class KnightMovementCalculatorSO : ChessUnitMovementCalculatorSO
    {
        public override IEnumerable<BoardCoordinate> CalculateMoveableCoordinates(BoardCoordinate origin, int mapSize, bool isFirstMove, bool isPlayerCoordinate)
        {
            BoardManager bm = BoardManager.Instance;

            for (int i = 0; i < moveTemplates.Length; i++)
            {
                BoardCoordinate moveTemplate = moveTemplates[i];
                BoardCoordinate movement = new BoardCoordinate();

                movement.column = origin.column + moveTemplate.column;
                movement.row = origin.row +  moveTemplate.row;

                if (bm.IsUnitInCoordinate(isPlayerCoordinate, movement))
                    continue;

                yield return movement;
            }
        }
    }
}
