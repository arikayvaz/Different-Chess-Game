namespace DCG.Gameplay
{
    using Common;
    using UnityEngine;

    public class PlayerController : Singleton<PlayerController>
    {
        [SerializeField] ChessUnitController unitController = null;

        public bool IsChessUnitSelected => unitController.IsChessUnitSelected;

        private void Start()
        {
            InitController();
        }

        public void InitController() 
        {
            unitController.InitConroller();
            unitController.SpawnChessUnits(true, ChessUnitColors.White);
            unitController.CalculateMoveableCoordinates();
        }

        public void PickChessUnit(ChessUnitBase chessUnit) 
        {
            unitController.PickChessUnit(chessUnit);
        }

        public void DropSelectedChessUnit() 
        {
            unitController.DropSelectedChessUnit();
        }

        public bool IsCoordinateOtherUnitMoveCoordinate(BoardCoordinate coordinate) 
        {
            return unitController.IsCoordinateOtherUnitMoveCoordinate(coordinate);
        }
    }
}