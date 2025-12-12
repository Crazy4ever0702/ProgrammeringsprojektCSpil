using UnityEngine;
using UnityEngine.EventSystems;   // <-- IMPORTANT

public class RoomClickManager : MonoBehaviour
{
    [Tooltip("Only colliders on these layers will be treated as interactable zones.")]
    public LayerMask interactableMask;

    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
        if (mainCam == null)
        {
            Debug.LogError("RoomClickManager: No main camera found in the scene.");
        }
    }

    private void Update()
    {
        if (mainCam == null) return;

        if (Input.GetMouseButtonDown(0))
        {
            // 1) If the pointer is currently over any UI, ignore this click for room interactions
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {
                // Debug.Log("Click over UI, not hitting room zones.");
                return;
            }

            HandleClick();
        }
    }

    private void HandleClick()
    {
        Vector3 worldPoint = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 point2D = new Vector2(worldPoint.x, worldPoint.y);

        RaycastHit2D hit = Physics2D.Raycast(point2D, Vector2.zero, Mathf.Infinity, interactableMask);

        if (hit.collider != null)
        {
            ClickableZone zone = hit.collider.GetComponent<ClickableZone>();
            if (zone != null)
            {
                zone.Interact();
            }
        }
    }
}