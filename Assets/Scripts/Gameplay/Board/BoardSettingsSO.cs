namespace DCG.Gameplay
{
    using UnityEngine;

    [CreateAssetMenu(fileName ="Board Settings", menuName = "DCG/Board/Board Settings")]
    public class BoardSettingsSO : ScriptableObject
    {
        public int mapSize = 8;
        public float pieceSize = 1.5f;

        public Vector3 GetWorldPosition(BoardCoordinate coordinate) 
        {
            return GetWorldPosition(coordinate.column, coordinate.row);
        }

        public Vector3 GetWorldPosition(int column, int row) 
        {
            return new Vector3(column * pieceSize, 0f, row * pieceSize);
        }

        public BoardCoordinate GetBoardCoordinate(Vector3 position) 
        {
            float fixedX = Mathf.Round(position.x);
            float fixedZ = Mathf.Round(position.z);

            int column = Mathf.RoundToInt(fixedX / pieceSize);
            int row = Mathf.RoundToInt(fixedZ / pieceSize);

            return new BoardCoordinate(column, row);
        }

        public bool IsCoordinateInBoardRegion(BoardCoordinate coordinate) 
        {
            return (coordinate.column >= 0 && coordinate.column < mapSize) && (coordinate.row >= 0 && coordinate.row < mapSize);
        }
    }
}