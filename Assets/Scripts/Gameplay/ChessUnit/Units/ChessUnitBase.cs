namespace DCG.Gameplay
{
    using UnityEngine;

    public abstract class ChessUnitBase : MonoBehaviour
    {
        public abstract ChessUnitTypes UnitType { get; }

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

        public virtual void InitUnit(BoardCoordinate coordinate, Material material) 
        {
            Coordinate = coordinate;
            meshRenderer.material = material;
        }
    }
}