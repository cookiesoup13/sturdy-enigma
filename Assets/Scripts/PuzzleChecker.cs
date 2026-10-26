using UnityEngine;

public class PuzzleChecker : MonoBehaviour
{
    public GameObject winScreen;

    public void Check()
    {
        bool solved = true;

        foreach (Piece p in Piece.All)
        {
            if (!p.IsCorrect())
            {
                solved = false;
            }
        }

        if (Piece.All.Count == 0)
        {
            solved = false;
        }

        if (solved)
        {
            Debug.Log("Solved");
            winScreen.SetActive(true);
        }
        else
        {
            Debug.Log("Not solved");
        }
    }
}