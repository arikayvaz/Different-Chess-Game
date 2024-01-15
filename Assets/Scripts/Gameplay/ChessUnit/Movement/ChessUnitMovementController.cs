namespace DCG.Gameplay
{
    using System.Collections.Generic;
    using Unity.VisualScripting;
    using UnityEngine;
    using static UnityEngine.UI.CanvasScaler;

    public class ChessUnitMovementController : MonoBehaviour
    {
        [SerializeField] PawnMovementCalculatorSO pawnMovementCalculator = null;
        [SerializeField] KnightMovementCalculatorSO knightMovementCalculator = null;
        [SerializeField] BishopMovementCalculatorSO bishopMovementCalculator = null;
        [SerializeField] RookMovementCalculatorSO rookMovementCalculator = null;
        [SerializeField] QueenMovementCalculatorSO queenMovementCalculator = null;
        [SerializeField] KingMovementCalculatorSO kingMovementCalculator = null;

        [Space]
        [SerializeField] GameObject goPreviewOrigin = null;
        [SerializeField] Transform trPreviewParent = null;

        private GameObject[] movementPreviews = null;

        public void InitController() 
        {
            int maxMoveableCoordCount = BoardManager.MapSize * 4 - 1;

            InitMovementPreviews(maxMoveableCoordCount);
            HideMovementPreviews();
        }

        public bool IsCoordinateOtherUnitMoveCoordinate(ChessUnitBase chessUnit, BoardCoordinate coordinate)
        {
            foreach (BoardCoordinate unitCoordinate in chessUnit.GetBoardCoordinates())
            {
                if (coordinate.IsEqual(unitCoordinate))
                    return true;
            }

            return false;
        }

        public void CalculateMoveableCoordinates(ChessUnitBase chessUnit) 
        {
            ChessUnitMovementCalculatorSO movementCalculator = GetMovementCalculator(chessUnit.UnitType);

            if (movementCalculator == null)
                return;

            bool isFirstMove = true; //TODO: Get is it first round

            /*
            List<BoardCoordinate> moveableCoordinates = null;
            unitMoveableCoordinates.TryGetValue(unitId, out moveableCoordinates);

            if (moveableCoordinates == null)
                moveableCoordinates = new List<BoardCoordinate>();

            moveableCoordinates.AddRange(movementCalculator.CalculateMoveableCoordinates(origin, BoardManager.MapSize, isFirstMove, true));
            */

            List<BoardCoordinate> moveableCoordinates = new List<BoardCoordinate>();
            moveableCoordinates.AddRange(movementCalculator.CalculateMoveableCoordinates(chessUnit.Coordinate, BoardManager.MapSize, isFirstMove, true));

            if (moveableCoordinates.Count < 1)
                return;

            BoardManager.Instance.ValidateCoordinates(ref moveableCoordinates);

            chessUnit.AddMoveableCoordinates(moveableCoordinates);
        }

        private ChessUnitMovementCalculatorSO GetMovementCalculator(ChessUnitTypes unitType) 
        {
            switch (unitType)
            {
                case ChessUnitTypes.Pawn:
                    return pawnMovementCalculator;
                case ChessUnitTypes.Knight:
                    return knightMovementCalculator;
                case ChessUnitTypes.Bishop:
                    return bishopMovementCalculator;
                case ChessUnitTypes.Rook:
                    return rookMovementCalculator;
                case ChessUnitTypes.Queen:
                    return queenMovementCalculator;
                case ChessUnitTypes.King:
                    return kingMovementCalculator;
                default:
                    return null;
            }
        }

        private void InitMovementPreviews(int maxMoveableCoordCount) 
        {
            movementPreviews = new GameObject[maxMoveableCoordCount];

            for (int i = 0; i < movementPreviews.Length; i++)
            {
                GameObject preview = Instantiate(goPreviewOrigin, trPreviewParent);

                preview.transform.position = Vector3.zero;
                preview.name = $"Movement Preview #{i + 1}";
                preview.gameObject.SetActive(false);

                movementPreviews[i] = preview;
            }
        }

        public void ShowMovementPreviews(ChessUnitBase chessUnit) 
        {
            HideMovementPreviews();

            BoardManager bm = BoardManager.Instance;

            int index = 0;
            foreach (BoardCoordinate coordinate in chessUnit.GetBoardCoordinates())
            {
                GameObject goPreview = movementPreviews[index];

                Vector3 pos = bm.GetWorldPosition(coordinate);

                pos.y += 0.5f;

                goPreview.transform.position = pos;
                goPreview.SetActive(true);

                index++;
            }
        }

        public void HideMovementPreviews() 
        {
            foreach (GameObject goPreview in movementPreviews)
                goPreview.SetActive(false);

        }
    }
}