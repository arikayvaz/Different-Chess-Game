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
        }

        public bool PickChessUnit(BoardCoordinate coordinate) 
        {
            return unitController.PickChessUnit(coordinate);
        }

        public void DropSelectedChessUnit() 
        {
            unitController.DropSelectedChessUnit();
        }
    }
}