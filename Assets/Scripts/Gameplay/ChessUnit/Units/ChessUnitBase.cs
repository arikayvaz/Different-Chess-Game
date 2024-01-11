namespace DCG.Gameplay
{
    using UnityEngine;

    public abstract class ChessUnitBase : MonoBehaviour
    {
        public abstract ChessUnitTypes UnitType { get; }

        [SerializeField] protected MeshRenderer meshRenderer = null;

        public virtual void InitUnit(Material material) 
        {
            meshRenderer.material = material;
        }
    }
}