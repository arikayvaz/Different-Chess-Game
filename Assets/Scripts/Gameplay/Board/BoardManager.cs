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

        public int MapSize => boardSettings.mapSize;
        public float PieceSize => boardSettings.pieceSize;

        private BoardCoordinate[,] boardMap = null;

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

        protected override void Awake()
        {
            base.Awake();

            InitBoardMap();
            board.InitBoard(boardMap, MapSize, PieceSize);
        }

        private void InitBoardMap() 
        {
            boardMap = new BoardCoordinate[MapSize, MapSize];

            for (int i = 0; i < boardMap.GetLength(0); i++)
            {
                for (int j = 0; j < boardMap.GetLength(1); j++)
                {
                    boardMap[i, j] = new BoardCoordinate(i, j);
                }
            }
        }
    }

    public struct BoardCoordinate 
    {
        public int row;
        public int column;

        public bool IsValid => row >= 0 && column >= 0;

        public BoardCoordinate(int row, int column)
        {
            this.row = row;
            this.column = column;
        }
    }

}