namespace DCG.Gameplay
{
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class ChessUnitBase : MonoBehaviour
    {
        public abstract ChessUnitTypes UnitType { get; }

        public int Id { get; private set; } = -1;
        public bool IsPlayerUnit { get; private set; } = false;


        [SerializeField] protected MeshRenderer meshRenderer = null;

        public BoardCoordinate Coordinate { get; private set; }

        public Vector3 Position => transform.position;
        public Vector3 TopPosition 
        {
            get 
            {
                Vector3 pos = Position;
                pos.y += meshRenderer.bounds.max.y;
                return pos;
            }
        }

        private List<BoardCoordinate> moveableCoordinates = null;

        public virtual void InitUnit(int id, bool isPlayerUnit, BoardCoordinate coordinate, Material material) 
        {
            Id = id;
            IsPlayerUnit = isPlayerUnit;
            Coordinate = coordinate;
            meshRenderer.material = material;

            moveableCoordinates = new List<BoardCoordinate>();
        }

        public void AddMoveableCoordinates(List<BoardCoordinate> coordinates) 
        {
            moveableCoordinates.AddRange(coordinates);
        }

        public void ClearMoveableCoordinates() 
        {
            moveableCoordinates.Clear();
        }

        public IEnumerable<BoardCoordinate> GetBoardCoordinates() 
        {
            if (moveableCoordinates == null || moveableCoordinates.Count < 1)
                yield return new BoardCoordinate(-1, -1);

            foreach (BoardCoordinate coordinate in moveableCoordinates)
            {
                yield return coordinate;
            }
        }
    }
}