using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePieceHolder : MonoBehaviour
{
    private string linkedPuzzlePieceName;

    public string LinkedPuzzlePieceName
    {
        get { return linkedPuzzlePieceName; }
        set { linkedPuzzlePieceName = value; }
    }

    private string setPuzzlePieceName;

    public string SetPuzzlePieceName
    {
        get { return setPuzzlePieceName; }
        set { setPuzzlePieceName = value; }
    }

    private void Awake()
    {
        setPuzzlePieceName = string.Empty;
    }
}
