namespace DCG.Gameplay
{
    using Common;
    using System.Diagnostics;
    using UnityEngine;

    public class InputManager : Singleton<InputManager>
    {
        [SerializeField] LayerMask worldPositionLayerMask;

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
                Vector3 worldPos = GetInputWorldPosition();

                PlayerController pc = PlayerController.Instance;
                BoardManager bm = BoardManager.Instance;

                BoardCoordinate coordinate = bm.GetBoardCoordinate(worldPos);

                if (pc.IsChessUnitSelected)
                {
                    if (!bm.IsCoordinateValid(coordinate)) 
                    {
                        if (pc.IsChessUnitSelected)
                            pc.DropSelectedChessUnit();

                        return;
                    }

                    //Try select new unit
                    bool isSelectionSuccess = pc.PickChessUnit(coordinate);

                    if (!isSelectionSuccess)
                    {
                        //Move unit
                    }

                    return;
                }

                //select unit
                pc.PickChessUnit(coordinate);
            }
        }

        private Vector3 GetInputWorldPosition() 
        {
            Vector3 worldPos = new Vector3(-1f, -1f, -1f);

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                worldPos = hit.point;
                worldPos.y = 0f;
            }

            return worldPos;
        }

    }
}