using UnityEngine;

public class RoomController : MonoBehaviour
{
    [Header("Puzzle Panels")]
    public GameObject chessPanel;
    public GameObject safePanel;
    public GameObject panel123Panel;
    public GameObject computerPanel;
    public GameObject bookshelfPanel;
    // add more as needed

    private void Start()
    {
        HideAllPanels();
    }

    public void OnZoneClicked(string zoneId)
    {
        HideAllPanels();

        switch (zoneId)
        {
            case "Chess":
                if (chessPanel != null)
                    chessPanel.SetActive(true);
                break;

            case "Safe":
                if (safePanel != null)
                    safePanel.SetActive(true);
                break;

            case "Panel123":
                if (panel123Panel != null)
                    panel123Panel.SetActive(true);
                break;

            case "Computer":
                if (computerPanel != null)
                    computerPanel.SetActive(true);
                break;

            case "Bookshelf":
                if (bookshelfPanel != null)
                    bookshelfPanel.SetActive(true);
                break;

            case "Door":
                // Later: check if all puzzles solved, open door / end game
                Debug.Log("Door clicked");
                break;

            case "Painting":
                // Maybe open painting zoom view or just show hint
                Debug.Log("Painting clicked");
                break;
        }
    }

    public void HideAllPanels()
    {
        if (chessPanel != null) chessPanel.SetActive(false);
        if (safePanel != null) safePanel.SetActive(false);
        if (panel123Panel != null) panel123Panel.SetActive(false);
        if (computerPanel != null) computerPanel.SetActive(false);
        if (bookshelfPanel != null) bookshelfPanel.SetActive(false);
    }
}