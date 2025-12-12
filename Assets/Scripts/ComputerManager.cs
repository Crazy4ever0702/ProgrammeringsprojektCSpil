using UnityEngine;
public class ComputerManager : MonoBehaviour
{
    public GameObject hintPanel;
    public GameObject tablePanel;

    public void OpenHint()
    {
        hintPanel.SetActive(true);
        tablePanel.SetActive(false);
    }

    public void OpenTable()
    {
        hintPanel.SetActive(false);
        tablePanel.SetActive(true);
    }

    public void CloseAll()
    {
        hintPanel.SetActive(false);
        tablePanel.SetActive(false);
        gameObject.SetActive(false); // closes computer
    }
}