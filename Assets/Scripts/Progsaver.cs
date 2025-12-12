using UnityEngine;
using UnityEngine.SceneManagement;
public class RoomController : MonoBehaviour
{
    [Header("Puzzle Panels")]
    public GameObject chessPanel;
    public GameObject panel123Panel;
    public GameObject bookshelfPanel;
    public GameObject computerPanel;
    public GameObject safePanel;
    public GameObject paintingPanel;
    public GameObject notePanel;
    public GameObject hintPanel;
    public GameObject periodicPanel;
    public GameObject winScreenPanel;
    private void Start()
    {
        HideAllPanels();
    }

    public void OnZoneClicked(string zoneId)
    {
        Debug.Log("OnZoneClicked called with: " + zoneId);

        HideAllPanels();

        switch (zoneId)
        {
            case "Chess":
                if (chessPanel != null)
                {
                    Debug.Log("Opening Chess Panel");
                    chessPanel.SetActive(true);
                }
                break;

            case "Panel123":
                if (panel123Panel != null)
                    panel123Panel.SetActive(true);
                break;

            case "Bookshelf":
                if (bookshelfPanel != null)
                    bookshelfPanel.SetActive(true);
                break;

            case "Computer":
                if (computerPanel != null)
                {
                    // You can restrict this later using GameManager.Instance.computerUnlocked
                    computerPanel.SetActive(true);
                }
                break;

            case "Safe":
                if (safePanel != null)
                    safePanel.SetActive(true);
                break;

            case "Painting":
                if (paintingPanel != null)
                    paintingPanel.SetActive(true);
                break;
            case "Note":
                if (notePanel != null) notePanel.SetActive(true);
                break;

            default:
                Debug.LogWarning("Unknown zoneId: " + zoneId);
                break;

            case "Door":
                if (GameManager.Instance != null && GameManager.Instance.doorUnlocked)
                {
                    // Door opened — show win screen
                    ShowWinScreen();
                }
                else
                {
                    Debug.Log("The door is locked.");
                    // Optional: show UI hint
                }
                break;
        }
    }
    public void ReturnToComputer()
    {
        // Close hint and periodic table
        if (hintPanel != null) hintPanel.SetActive(false);
        if (periodicPanel != null) periodicPanel.SetActive(false);

        // Re-open the main computer screen
        if (computerPanel != null) computerPanel.SetActive(true);

        Debug.Log("Returned to Computer Panel");
    }
    public void HideAllPanels()
    {
        if (chessPanel != null) chessPanel.SetActive(false);
        if (panel123Panel != null) panel123Panel.SetActive(false);
        if (bookshelfPanel != null) bookshelfPanel.SetActive(false);
        if (computerPanel != null) computerPanel.SetActive(false);
        if (safePanel != null) safePanel.SetActive(false);
        if (paintingPanel != null) paintingPanel.SetActive(false);
        if (notePanel != null) notePanel.SetActive(false);
    }

    public void ShowWinScreen()
    {
        if (winScreenPanel != null)
            winScreenPanel.SetActive(true);

        // Disable room interaction while win screen is open
        if (Camera.main != null)
            Camera.main.GetComponent<RoomClickManager>().enabled = false;

        Debug.Log("Win screen opened.");
    }
    public void PlayAgain()
    {
        // Reload the active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}