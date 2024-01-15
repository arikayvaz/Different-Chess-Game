namespace DCG.Gameplay
{
    using System.Collections.Generic;
    using UnityEngine;

    public enum ChessUnitTypes { Pawn, Knight, Bishop, Rook, Queen, King }
    public enum ChessUnitColors { White, Black }

    public class ChessUnitController : MonoBehaviour
    {
        [SerializeField] ChessUnitSettingsSO unitSettings = null;
        [SerializeField] ChessUnitSelectionController selectionController = null;
        [SerializeField] ChessUnitMovementController movementController = null;

        public bool IsChessUnitSelected => selectionController.IsChessUnitSelected;

        private Dictionary<BoardCoordinate, ChessUnitBase> chessUnits = null;

        public void InitConroller() 
        {
            selectionController.InitController();
            movementController.InitController();
        }


        public void SpawnChessUnits(bool isPlayer, ChessUnitColors chessUnitColor) 
        {
            BoardManager bm = BoardManager.Instance;
            int mapSize = BoardManager.MapSize;

            chessUnits = new Dictionary<BoardCoordinate, ChessUnitBase>(mapSize * 2);
            Material unitMaterial = unitSettings.GetUnitMaterial(chessUnitColor);

            //Spawn pawns
            int pawnRowIndex = unitSettings.GetPawnRowIndex(isPlayer);

            BoardCoordinate coordinate = new BoardCoordinate(0, pawnRowIndex);

            int unitId = 0;

            for (int i = 0; i < mapSize; i++)
            {
                coordinate.column = i;
                ChessUnitBase chessUnit = Instantiate<ChessUnitBase>(unitSettings.chessUnitPawn, bm.GetWorldPosition(coordinate), Quaternion.identity, transform);
                chessUnit.InitUnit(unitId, true, coordinate, unitMaterial);
                chessUnits.Add(coordinate, chessUnit);

                bm.SetPlayerChessUnitCooradinate(chessUnit.Id, chessUnit.Coordinate);
                unitId++;
            }

            //Spawn units
            int unitRowIndex = unitSettings.GetUnitRowIndex(isPlayer);
            coordinate = new BoardCoordinate(0, unitRowIndex);
            ChessUnitBase[] units = unitSettings.chessUnits;

            for (int i = 0; i < units.Length; i++)
            {
                coordinate.column = i;
                ChessUnitBase chessUnit = Instantiate<ChessUnitBase>(units[i], bm.GetWorldPosition(coordinate), Quaternion.identity, transform);
                chessUnit.InitUnit(unitId, true, coordinate, unitMaterial);
                chessUnits.Add(coordinate, chessUnit);

                bm.SetPlayerChessUnitCooradinate(chessUnit.Id, chessUnit.Coordinate);
                unitId++;
            }
        }

        public void PickChessUnit(ChessUnitBase chessUnit) 
        {
            selectionController.SelectChessUnit(chessUnit);
            movementController.ShowMovementPreviews(chessUnit);
        }

        public void DropSelectedChessUnit() 
        {
            selectionController.DropSelectedChessUnit();
            movementController.HideMovementPreviews();
        }

        public void CalculateMoveableCoordinates() 
        {
            foreach (ChessUnitBase chessUnit in chessUnits.Values)
            {
                movementController.CalculateMoveableCoordinates(chessUnit);
            }
        }

        public bool IsCoordinateOtherUnitMoveCoordinate(BoardCoordinate coordinate) 
        {
            foreach (ChessUnitBase chessUnit in chessUnits.Values) 
            {
                if (movementController.IsCoordinateOtherUnitMoveCoordinate(chessUnit, coordinate))
                    return true;
            }

            return false;
                
        }

        private ChessUnitBase GetChessUnit(BoardCoordinate coordinate)
        {
            if (chessUnits == null || chessUnits.Count == 0)
                return null;

            if (!BoardManager.Instance.IsCoordinateValid(coordinate))
                return null;

            ChessUnitBase unit = null;
            chessUnits.TryGetValue(coordinate, out unit);

            return unit;
        }
    }
}