namespace DCG.Gameplay
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Board : MonoBehaviour
    {
        [SerializeField] GameObject goWhitePiece = null;
        [SerializeField] GameObject goBlackPiece = null;

        public void InitBoard(BoardCoordinate[,] boardMap) 
        {
            for (int i = 0; i < boardMap.GetLength(0); i++)
            {
                for (int j = 0; j < boardMap.GetLength(1); j++)
                {
                    GameObject goOriginal = (i + j) % 2 == 0 ? goWhitePiece : goBlackPiece;

                    Instantiate(goOriginal, boardMap[i, j].Position, Quaternion.identity, transform);
                }
            }
        }
    }
}