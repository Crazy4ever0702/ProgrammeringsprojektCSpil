using UnityEngine;
using TMPro;

public class SafeLockController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text[] digitTexts;   // size 4
    [SerializeField] private TMP_Text statusText;     // optional

    [Header("Code")]
    [SerializeField] private string correctCode = "0287";

    private int[] digits = new int[4];
    private bool solved = false;

    private void OnEnable()
    {
        // Reset each time you open the safe, unless already solved
        if (!solved)
        {
            for (int i = 0; i < digits.Length; i++)
                digits[i] = 0;

            UpdateAllTexts();
            UpdateStatus("Enter the code.");
        }
        else
        {
            UpdateStatus("The safe is already open.");
        }
    }

    // Called from the digit buttons
    public void IncrementDigit(int index)
    {
        if (solved) return;
        if (index < 0 || index >= digits.Length) return;

        digits[index] = (digits[index] + 1) % 10;
        digitTexts[index].text = digits[index].ToString();

        CheckCode();
    }

    private void CheckCode()
    {
        // Build current code string
        string current = "";
        for (int i = 0; i < digits.Length; i++)
            current += digits[i].ToString();

        if (current == correctCode)
        {
            OnCorrectCode();
        }
    }

    private void OnCorrectCode()
    {
        solved = true;
        UpdateStatus("You hear the safe unlock.");

        // Mark safe / door as unlocked in GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.safeOpened = true;
            GameManager.Instance.doorUnlocked = true;   // if you want door access here
        }

        Debug.Log("Safe code correct. Safe opened.");
    }

    private void UpdateAllTexts()
    {
        for (int i = 0; i < digits.Length; i++)
        {
            if (digitTexts != null && i < digitTexts.Length && digitTexts[i] != null)
                digitTexts[i].text = digits[i].ToString();
        }
    }

    private void UpdateStatus(string message)
    {
        if (statusText != null)
            statusText.text = message;
    }
}