namespace DCG.Gameplay
{
    using Common;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    public class BoardManager : Singleton<BoardManager>
    {
        [SerializeField] BoardSettingsSO boardSettings = null;
        [SerializeField] Board board = null;

        const int MAP_SIZE_DEFAULT = 8;
        public static int MapSize => Instance?.boardSettings?.mapSize ?? MAP_SIZE_DEFAULT;

        const float PIECE_SIZE_DEFAULT = 1.5f;
        public static float PieceSize => Instance?.boardSettings?.pieceSize ?? PIECE_SIZE_DEFAULT;


        private BoardCoordinate[] playerChessUnitCoordinates = null;

        protected override void Awake()
        {
            base.Awake();

            BoardCoordinate[,] boardMap = SetBoardMap(MapSize);
            board.InitBoard(boardMap, MapSize, PieceSize);

            playerChessUnitCoordinates = new BoardCoordinate[16];
        }

        private BoardCoordinate[,] SetBoardMap(int mapSize)
        {
            BoardCoordinate[,] boardMap = new BoardCoordinate[mapSize, mapSize];

            for (int j = 0; j < boardMap.GetLength(1); j++)
                for (int i = 0; i < boardMap.GetLength(0); i++)
                    boardMap[i, j] = new BoardCoordinate(i, j);

            return boardMap;
        }

        public Vector3 GetWorldPosition(BoardCoordinate coordinate) 
        {
            return boardSettings.GetWorldPosition(coordinate);
        }

        public BoardCoordinate GetBoardCoordinate(Vector3 position) 
        {
            return boardSettings.GetBoardCoordinate(position);
        }

        public bool IsCoordinateValid(BoardCoordinate coordinate) 
        {
            if (!coordinate.IsValid)
                return false;

            return coordinate.row <= boardSettings.mapSize - 1 && coordinate.column <= boardSettings.mapSize - 1;
        }

        public void ValidateCoordinates(ref List<BoardCoordinate> coordinates) 
        {
            List<BoardCoordinate> removeList = new List<BoardCoordinate>();

            for (int i = 0; i < coordinates.Count; i++)
            {
                BoardCoordinate coordinate = coordinates[i];

                if (boardSettings.IsCoordinateInBoardRegion(coordinate))
                    continue;

                removeList.Add(coordinate);
            }

            if (removeList.Count < 1)
                return;

            foreach (BoardCoordinate coordinate in removeList)
                coordinates.Remove(coordinate);

        }

        public void SetPlayerChessUnitCooradinate(int unitId, BoardCoordinate coordinate) 
        {
            if (unitId < 0 || unitId > playerChessUnitCoordinates.Length - 1)
                return;

            playerChessUnitCoordinates[unitId] = coordinate;
        }

        public bool IsUnitInCoordinate(bool isPlayer, BoardCoordinate coordinate)
        {
            if (isPlayer)
                return IsPlayerChessUnitInCoordinate(coordinate);
            else
                return false;
        }

        private bool IsPlayerChessUnitInCoordinate(BoardCoordinate coordinate)
        {
            for (int i = 0; i < playerChessUnitCoordinates.Length; i++)
            {
                if (!playerChessUnitCoordinates[i].IsEqual(coordinate))
                    continue;

                return true;
            }

            return false;
        }

        public bool IsCoordinateOtherUnitMoveCoordinate(bool isPlayer, BoardCoordinate coordinate) 
        {
            if (isPlayer)
                return PlayerController.Instance.IsCoordinateOtherUnitMoveCoordinate(coordinate);
            else
                return false;
        }

    }

    [System.Serializable]
    public struct BoardCoordinate 
    {
        public int column;
        public int row;

        public bool IsValid => column >= 0 && row >= 0;

        public BoardCoordinate(int column, int row)
        {
            this.column = column;
            this.row = row;
        }

        public bool IsEqual(BoardCoordinate coordinate) 
        {
            return column == coordinate.column && row == coordinate.row;
        }
    }

}