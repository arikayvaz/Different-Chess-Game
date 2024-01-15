namespace DCG.Gameplay
{
    using Common;
    using System.Diagnostics;
    using UnityEngine;

    public class InputManager : Singleton<InputManager>
    {
        [SerializeField] LayerMask worldPositionLayerMask;
        [SerializeField] LayerMask chestUnitSelectLayerMask;

        Camera mainCamera;

        protected override void Awake()
        {
            base.Awake();

            mainCamera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) 
            {
                TrySelectChestUnit();
            }
        }

        private void TrySelectChestUnit() 
        {
            PlayerController pc = PlayerController.Instance;
            BoardManager bm = BoardManager.Instance;

            ChessUnitBase inputUnit = GetChessUnitAtInputPosition(Input.mousePosition);

            if (inputUnit != null)
            {
                //There is a chess unit in input position

                if (inputUnit.IsPlayerUnit)
                {
                    //clicked unit is player unit

                    if (pc.IsChessUnitSelected)
                        pc.DropSelectedChessUnit();

                    pc.PickChessUnit(inputUnit);
                    return;
                }

                //clicked unit is opponent unit
                if (pc.IsChessUnitSelected)
                {
                    // Attack...
                }

                return;
            }//if (inputUnit != null)

            //There is no chess unit in input position

            if (pc.IsChessUnitSelected)
            {
                //Player has a chess unit in its hand

                Vector3 worldPos = GetInputWorldPosition();
                BoardCoordinate coordinate = bm.GetBoardCoordinate(worldPos);

                if (bm.IsCoordinateValid(coordinate))
                {
                   //Input pos is a valid coordinate
                   //Move...
                }else 
                {
                    //Input pos is not a valid coordinate
                    if (pc.IsChessUnitSelected)
                        pc.DropSelectedChessUnit();

                }
            }
        }

        private ChessUnitBase GetChessUnitAtInputPosition(Vector3 inputPos) 
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, chestUnitSelectLayerMask))
                return hit.collider.GetComponent<ChessUnitBase>();

            return null;
        }

        private Vector3 GetInputWorldPosition() 
        {
            Vector3 worldPos = new Vector3(-1f, -1f, -1f);

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, worldPositionLayerMask))
            {
                worldPos = hit.point;
                worldPos.y = 0f;
            }

            return worldPos;
        }

    }
}