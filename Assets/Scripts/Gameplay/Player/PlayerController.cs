namespace DCG.Gameplay
{
    using Common;
    using UnityEngine;

    public class PlayerController : Singleton<PlayerController>
    {
        [SerializeField] ChessUnitController unitController = null;

        private void Start()
        {
            InitController();
        }

        public void InitController() 
        {
            unitController.SpawnChessUnits(true, ChessUnitColors.White);
        }
    }
}