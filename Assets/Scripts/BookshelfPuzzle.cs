using UnityEngine;
using TMPro;   // or use UnityEngine.UI if you prefer

public class BookshelfPuzzle : MonoBehaviour
{
    [Header("Correct order of book IDs")]
    [SerializeField] private int[] correctOrder = { 1, 2, 3, 4 };

    [Header("UI")]
    [SerializeField] private TMP_Text feedbackText;

    private int currentIndex = 0;
    private bool solved = false;

    private void OnEnable()
    {
        if (!solved)
        {
            currentIndex = 0;
            UpdateFeedback("The volumes feel out of order.");
        }
    }

    public void ClickBook(int bookId)
    {
        if (solved) return;

        // Check if this click matches the next expected ID
        if (bookId == correctOrder[currentIndex])
        {
            currentIndex++;

            if (currentIndex >= correctOrder.Length)
            {
                Solve();
            }
            else
            {
                UpdateFeedback($"Book {currentIndex} placed correctly.");
            }
        }
        else
        {
            // Wrong book, reset sequence
            currentIndex = 0;
            UpdateFeedback("That order doesn’t fit. Try again.");
        }
    }

    private void Solve()
    {
        solved = true;
        UpdateFeedback("The shelf settles with a quiet thud.");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.bookshelfSolved = true;
            GameManager.Instance.RecalculateComputerUnlock();
        }

        Debug.Log("Bookshelf puzzle solved!");
    }

    private void UpdateFeedback(string message)
    {
        if (feedbackText != null)
            feedbackText.text = message;
    }
}