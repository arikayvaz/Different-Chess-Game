namespace DCG.Gameplay
{
    using System.Collections.Generic;
    using UnityEngine;

    public enum ChessUnitTypes { Pawn, Knight, Bishop, Rook, Queen, King }
    public enum ChessUnitColors { White, Black }

    public class ChessUnitController : MonoBehaviour
    {
        [SerializeField] ChessUnitSettingsSO unitSettings = null;

        private ChessUnitBase[,] chessUnits = null;

        public void SpawnChessUnits(bool isPlayer, ChessUnitColors chessUnitColor) 
        {
            chessUnits = new ChessUnitBase[2, BoardManager.MAP_SIZE];
            Material unitMaterial = unitSettings.GetUnitMaterial(chessUnitColor);

            //Spawn pawns
            int pawnRowIndex = unitSettings.GetPawnRowIndex(isPlayer);

            BoardCoordinate pawnCoord = new BoardCoordinate(pawnRowIndex, 0);

            for (int i = 0; i < BoardManager.MAP_SIZE; i++)
            {
                pawnCoord.column = i;
                ChessUnitBase chessUnit = Instantiate<ChessUnitBase>(unitSettings.chessUnitPawn, pawnCoord.Position, Quaternion.identity, transform);
                chessUnit.InitUnit(unitMaterial);
                chessUnits[pawnCoord.row, pawnCoord.column] = chessUnit;
            }

            //Spawn units
            int unitRowIndex = unitSettings.GetUnitRowIndex(isPlayer);
            pawnCoord = new BoardCoordinate(unitRowIndex, 0);
            ChessUnitBase[] units = unitSettings.chessUnits;

            for (int i = 0; i < units.Length; i++)
            {
                pawnCoord.column = i;
                ChessUnitBase chessUnit = Instantiate<ChessUnitBase>(units[i], pawnCoord.Position, Quaternion.identity, transform);
                chessUnit.InitUnit(unitMaterial);
                chessUnits[pawnCoord.row, pawnCoord.column] = chessUnit;
            }
        }
    }
}