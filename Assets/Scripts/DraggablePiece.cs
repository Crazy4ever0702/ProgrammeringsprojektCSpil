using UnityEngine;
using UnityEngine.EventSystems;

public class DraggablePiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] public ChessPuzzleManager puzzleManager;
    public bool canDrag = true;

    private RectTransform rectTransform;
    private RectTransform parentRect;
    private Vector2 originalAnchoredPosition;
    private Vector2 dragOffset;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        parentRect = rectTransform.parent as RectTransform;
    }

    public void LockPiece()
    {
        canDrag = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!canDrag || puzzleManager == null) return;

        originalAnchoredPosition = rectTransform.anchoredPosition;

        Vector2 localMousePos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentRect,
                eventData.position,
                eventData.pressEventCamera,
                out localMousePos))
        {
            // Offset so the piece doesn’t jump its center to the mouse
            dragOffset = rectTransform.anchoredPosition - localMousePos;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!canDrag || puzzleManager == null) return;

        Vector2 localMousePos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentRect,
                eventData.position,
                eventData.pressEventCamera,
                out localMousePos))
        {
            rectTransform.anchoredPosition = localMousePos + dragOffset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!canDrag || puzzleManager == null) return;

        bool moveAccepted;
        Vector3 snapWorldPos;

        puzzleManager.TryApplyMove(this, rectTransform.position, out moveAccepted, out snapWorldPos);

        if (moveAccepted)
        {
            rectTransform.position = snapWorldPos;
        }
        else
        {
            rectTransform.anchoredPosition = originalAnchoredPosition;
        }
    }
}