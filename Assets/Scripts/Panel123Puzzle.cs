using UnityEngine;
using TMPro;   // If you use TextMeshPro; otherwise swap to UnityEngine.UI.Text

public class Panel123Puzzle : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private string correctSequence = "233113";  // example from painting
    [SerializeField] private int maxLength = 12;

    [Header("UI")]
    [SerializeField] private TMP_Text feedbackText;  // optional, can be null

    private string currentSequence = "";
    private bool solved = false;

    private void OnEnable()
    {
        // Reset each time the panel is opened
        if (!solved)
        {
            currentSequence = "";
            UpdateFeedback();
        }
    }

    public void PressButton(int value)
    {
        if (solved) return;

        // Append the pressed number
        currentSequence += value.ToString();

        // Optional: prevent unbounded length
        if (currentSequence.Length > maxLength)
        {
            currentSequence = currentSequence.Substring(currentSequence.Length - maxLength);
        }

        // Check if we hit the exact sequence
        if (currentSequence == correctSequence)
        {
            Solve();
        }
        else if (!correctSequence.StartsWith(currentSequence))
        {
            // If the current input cannot possibly match anymore, reset
            currentSequence = "";
        }

        UpdateFeedback();
    }

    private void Solve()
    {
        solved = true;

        if (feedbackText != null)
        {
            feedbackText.text = "You hear a faint humming... something powered on.";
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.panel123Solved = true;
            GameManager.Instance.RecalculateComputerUnlock();
        }

        Debug.Log("Panel123 puzzle solved!");
    }

    private void UpdateFeedback()
    {
        if (feedbackText == null) return;

        if (solved)
        {
            // already set in Solve()
            return;
        }

        if (currentSequence.Length == 0)
            feedbackText.text = "Enter the color code.";
        else
            feedbackText.text = currentSequence;  // or "***" if you want it hidden
    }
}