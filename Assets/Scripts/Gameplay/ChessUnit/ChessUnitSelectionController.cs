namespace DCG.Gameplay
{
    using UnityEngine;

    public class ChessUnitSelectionController : MonoBehaviour
    {
        [SerializeField] GameObject goSelectionVisual = null;

        public bool IsChessUnitSelected => selectedChessUnit != null;
        private ChessUnitBase selectedChessUnit = null;

        public void InitController() 
        {
            HideSelection();
        }

        public void SelectChessUnit(ChessUnitBase chessUnitBase) 
        {
            if (IsChessUnitSelected)
                DropSelectedChessUnit();

            selectedChessUnit = chessUnitBase;
            ShowSelection(selectedChessUnit.TopPosition);
        }

        public void DropSelectedChessUnit() 
        {
            selectedChessUnit = null;
            HideSelection();
        }

        const float SELECTION_POS_Y_DELTA = 1f;
        private void ShowSelection(Vector3 unitPos) 
        {
            Vector3 pos = unitPos;
            pos.y += SELECTION_POS_Y_DELTA;

            goSelectionVisual.transform.position = pos;
            goSelectionVisual.SetActive(true);
        }

        private void HideSelection() 
        {
            goSelectionVisual.SetActive(false);
        }
    }
}