namespace DCG.Gameplay
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "ChessUnitSettings", menuName = "DCG/Chess Unit Settings")]
    public class ChessUnitSettingsSO : ScriptableObject
    {
        [Header("Player")]
        public int playerPawnSpawnRowIndex = 0;
        public int playerUnitSpawnRowIndex = 0;

        [Space]
        [Header("Opponent")]
        public int opponentPawnSpawnRowIndex = 0;
        public int opponentUnitSpawnRowIndex = 0;

        [Space]
        [Header("Unit Colors")]
        public Material whiteUnitMaterial = null;
        public Material blackUnitMaterial = null;

        [Space]
        [Header("Units")]
        public ChessUnitBase chessUnitPawn = null;
        public ChessUnitBase[] chessUnits = new ChessUnitBase[8];

        public Material GetUnitMaterial(ChessUnitColors unitColor) 
        {
            switch (unitColor)
            {
                case ChessUnitColors.White:
                    return whiteUnitMaterial;
                case ChessUnitColors.Black:
                    return blackUnitMaterial;
                default:
                    return null;
            }
        }

        public int GetPawnRowIndex(bool isPlayer) 
        {
            return isPlayer ? playerPawnSpawnRowIndex : opponentPawnSpawnRowIndex;
        }

        public int GetUnitRowIndex(bool isPlayer) 
        {
            return isPlayer ? playerUnitSpawnRowIndex : opponentUnitSpawnRowIndex;
        }
    }
}