using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Room puzzles")]
    public bool chessSolved = false;        // chessboard puzzle
    public bool panel123Solved = false;     // wall 1-2-3 panel
    public bool bookshelfSolved = false;    // bookshelf / roman numerals puzzle

    [Header("Computer / meta states")]
    public bool computerUnlocked = false;   // becomes true when all three room puzzles are solved
    public bool computerPuzzleSolved = false;   // if you want a final computer step (optional)

    [Header("Safe / door states")]
    public bool safeOpened = false;         // set true when safe code is correct
    public bool doorUnlocked = false;       // set true when player can open the door

    private void Awake()
    {
        // Simple singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Call this whenever any of the three room puzzles (chess, panel123, bookshelf) changes state.
    /// </summary>
    public void RecalculateComputerUnlock()
    {
        computerUnlocked = chessSolved && panel123Solved && bookshelfSolved;
        // Debug.Log("Computer unlocked state: " + computerUnlocked);
    }
}