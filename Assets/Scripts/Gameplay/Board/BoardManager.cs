namespace DCG.Gameplay
{
    using Common;
    using UnityEditor;
    using UnityEngine;

    public class BoardManager : Singleton<BoardManager>
    {
        [SerializeField] Board board = null;

        public const int MAP_SIZE = 8;
        public const float PIECE_SIZE = 1f;

        private BoardCoordinate[,] boardMap = null;

        protected override void Awake()
        {
            base.Awake();

            InitBoardMap();
            board.InitBoard(boardMap);
        }

        private void InitBoardMap() 
        {
            boardMap = new BoardCoordinate[MAP_SIZE, MAP_SIZE];

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

        public Vector3 Position => new Vector3(column * BoardManager.PIECE_SIZE, 0f, row * BoardManager.PIECE_SIZE);

        public BoardCoordinate(int row, int column)
        {
            this.row = row;
            this.column = column;
        }
    }

}