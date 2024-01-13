namespace DCG.Gameplay
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Board : MonoBehaviour
    {
        [SerializeField] GameObject goWhitePiece = null;
        [SerializeField] GameObject goBlackPiece = null;

        [Space]
        [SerializeField] BoxCollider worldCollider = null;

        public void InitBoard(BoardCoordinate[,] boardMap, int mapSize, float pieceSize) 
        {
            SpawnBoardTiles(boardMap);
            SetWorldColliderSize(mapSize, pieceSize);
        }

        private void SpawnBoardTiles(BoardCoordinate[,] boardMap) 
        {
            BoardManager bm = BoardManager.Instance;

            for (int i = 0; i < boardMap.GetLength(0); i++)
            {
                for (int j = 0; j < boardMap.GetLength(1); j++)
                {
                    GameObject goOriginal = (i + j) % 2 == 0 ? goWhitePiece : goBlackPiece;
                    Vector3 position = bm.GetWorldPosition(boardMap[i, j]);

                    Instantiate(goOriginal, position, Quaternion.identity, transform);
                }
            }
        }

        private void SetWorldColliderSize(int mapSize, float pieceSize) 
        {
            const float WORLD_COLLIDER_HEIGHT = 2f;

            float size = mapSize * pieceSize;
            float offset = ((mapSize - 1) * pieceSize) / 2f;

            worldCollider.size = new Vector3(size, WORLD_COLLIDER_HEIGHT, size);
            worldCollider.center = new Vector3(offset, -WORLD_COLLIDER_HEIGHT / 2f, offset);
        }
    }
}