namespace DCG.Gameplay
{
    using System.Collections.Generic;
    using UnityEngine;

    //[CreateAssetMenu(fileName = "BaseMovementCalculator", menuName = "DCG/Chess Unit/Movement/Base Movement Calculator")]
    public abstract class ChessUnitMovementCalculatorSO : ScriptableObject
    {
        [SerializeField] protected BoardCoordinate[] moveTemplates = null;

        public abstract IEnumerable<BoardCoordinate> CalculateMoveableCoordinates(BoardCoordinate origin, int mapSize, bool isFirstMove, bool isPlayerCoordinate);
    }
}