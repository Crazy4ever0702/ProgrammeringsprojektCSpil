using System.Collections;
using UnityEngine;

public class ChessPuzzleManager : MonoBehaviour
{
    public enum PuzzleStage { FirstMove, SecondMove, Solved }

    [Header("Key pieces")]
    public DraggablePiece whiteQueen;
    public DraggablePiece whiteKnight;
    public RectTransform blackRookF8;      // starting location of the rook on f8

    [Header("Target squares")]
    public RectTransform squareQg8;
    public RectTransform squareNf7;
    public RectTransform squareG8ForRook;

    [Header("UI")]
    public GameObject successText;

    [Header("Settings")]
    public float snapDistance = 40f;

    private PuzzleStage stage = PuzzleStage.FirstMove;

    private void Start()
    {
        // Register this manager on the draggable pieces you care about
        if (whiteQueen != null) whiteQueen.puzzleManager = this;
        if (whiteKnight != null) whiteKnight.puzzleManager = this;

        // If you added DraggablePiece to other white pieces,
        // assign puzzleManager to them too so they snap back:
        foreach (var piece in GetComponentsInChildren<DraggablePiece>())
        {
            if (piece != whiteQueen && piece != whiteKnight)
            {
                piece.puzzleManager = this;
            }
        }

        if (successText != null)
            successText.SetActive(false);
    }

    public void TryApplyMove(
        DraggablePiece piece,
        Vector3 worldDropPos,
        out bool moveAccepted,
        out Vector3 snapWorldPos)
    {
        moveAccepted = false;
        snapWorldPos = Vector3.zero;

        switch (stage)
        {
            case PuzzleStage.FirstMove:
                HandleFirstMove(piece, worldDropPos, ref moveAccepted, ref snapWorldPos);
                break;

            case PuzzleStage.SecondMove:
                HandleSecondMove(piece, worldDropPos, ref moveAccepted, ref snapWorldPos);
                break;

            case PuzzleStage.Solved:
                // Puzzle is done; don’t allow further real moves
                break;
        }
    }

    private void HandleFirstMove(
        DraggablePiece piece,
        Vector3 worldDropPos,
        ref bool moveAccepted,
        ref Vector3 snapWorldPos)
    {
        // Only the queen to g8 is accepted for move 1
        if (piece == whiteQueen && IsCloseTo(worldDropPos, squareQg8.position))
        {
            moveAccepted = true;
            snapWorldPos = squareQg8.position;

            stage = PuzzleStage.SecondMove;

            // Simulate ...Rxg8 capturing the queen
            StartCoroutine(BlackReply_Rxg8());
        }
        // any other move is automatically rejected; DraggablePiece will snap back
    }

    private void HandleSecondMove(
        DraggablePiece piece,
        Vector3 worldDropPos,
        ref bool moveAccepted,
        ref Vector3 snapWorldPos)
    {
        // Only the knight to f7 finishes the puzzle
        if (piece == whiteKnight && IsCloseTo(worldDropPos, squareNf7.position))
        {
            moveAccepted = true;
            snapWorldPos = squareNf7.position;

            stage = PuzzleStage.Solved;
            OnPuzzleSolved();
        }
        // all other moves are rejected
    }

    private bool IsCloseTo(Vector3 a, Vector3 b)
    {
        return Vector2.Distance(a, b) <= snapDistance;
    }

    private IEnumerator BlackReply_Rxg8()
    {
        // Wait a short moment so the queen visibly lands on g8 first
        yield return new WaitForSeconds(0.3f);

        // Move rook from f8 to g8
        if (blackRookF8 != null && squareG8ForRook != null)
        {
            blackRookF8.position = squareG8ForRook.position;
        }

        // Hide white queen to show it was captured
        if (whiteQueen != null)
        {
            whiteQueen.gameObject.SetActive(false);
            whiteQueen.LockPiece();
        }
    }

    private void OnPuzzleSolved()
    {
        // Lock knight and any other draggable pieces
        if (whiteKnight != null) whiteKnight.LockPiece();
        if (whiteQueen != null) whiteQueen.LockPiece();

        // Show cryptic hint text
        if (successText != null)
            successText.SetActive(true);

        // Inform global game state
        if (GameManager.Instance != null)
            GameManager.Instance.chessSolved = true;

        Debug.Log("Chess puzzle solved: Qg8+ Rxg8 Nf7#");
    }
}