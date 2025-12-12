using UnityEngine;

public class ClickableZone : MonoBehaviour
{
    [Tooltip("Identifier used by RoomController (e.g. 'Chess', 'Panel123', 'Bookshelf')")]
    public string zoneId;

    [Tooltip("Reference to the RoomController in this scene")]
    public RoomController controller;

    // Called by RoomClickManager when this zone is clicked
    public void Interact()
    {
        if (controller == null)
        {
            Debug.LogWarning($"ClickableZone on {name} has no RoomController assigned.");
            return;
        }

        if (string.IsNullOrEmpty(zoneId))
        {
            Debug.LogWarning($"ClickableZone on {name} has an empty zoneId.");
            return;
        }

        Debug.Log($"Clicked zone: {zoneId}");
        controller.OnZoneClicked(zoneId);
    }
}