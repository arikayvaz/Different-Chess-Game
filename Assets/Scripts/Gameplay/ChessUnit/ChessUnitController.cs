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

        public bool IsChessUnitSelected => selectionController.IsChessUnitSelected;

        private Dictionary<BoardCoordinate, ChessUnitBase> chessUnits = null;

        public void InitConroller() 
        {
            selectionController.InitController();
        }


        public void SpawnChessUnits(bool isPlayer, ChessUnitColors chessUnitColor) 
        {
            BoardManager bm = BoardManager.Instance;
            int mapSize = bm.MapSize;

            chessUnits = new Dictionary<BoardCoordinate, ChessUnitBase>(mapSize * 2);
            Material unitMaterial = unitSettings.GetUnitMaterial(chessUnitColor);

            //Spawn pawns
            int pawnRowIndex = unitSettings.GetPawnRowIndex(isPlayer);

            BoardCoordinate coordinate = new BoardCoordinate(pawnRowIndex, 0);

            for (int i = 0; i < mapSize; i++)
            {
                coordinate.column = i;
                ChessUnitBase chessUnit = Instantiate<ChessUnitBase>(unitSettings.chessUnitPawn, bm.GetWorldPosition(coordinate), Quaternion.identity, transform);
                chessUnit.InitUnit(coordinate, unitMaterial);
                chessUnits.Add(coordinate, chessUnit);
            }

            //Spawn units
            int unitRowIndex = unitSettings.GetUnitRowIndex(isPlayer);
            coordinate = new BoardCoordinate(unitRowIndex, 0);
            ChessUnitBase[] units = unitSettings.chessUnits;

            for (int i = 0; i < units.Length; i++)
            {
                coordinate.column = i;
                ChessUnitBase chessUnit = Instantiate<ChessUnitBase>(units[i], bm.GetWorldPosition(coordinate), Quaternion.identity, transform);
                chessUnit.InitUnit(coordinate, unitMaterial);
                chessUnits.Add(coordinate, chessUnit);
            }
        }

        public bool PickChessUnit(BoardCoordinate coordinate) 
        {
            ChessUnitBase chessUnit = GetChessUnit(coordinate);

            if (chessUnit == null)
                return false;

            selectionController.SelectChessUnit(chessUnit);
            return true;
        }

        public void DropSelectedChessUnit() 
        {
            selectionController.DropSelectedChessUnit();
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