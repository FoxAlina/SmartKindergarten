using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PuzzlePiece : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]
    PuzzleGameManager puzzleGameManager;

    private string linkedPuzzlePieceHolderName;

    public string LinkedPuzzlePieceHolderName
    {
        get { return linkedPuzzlePieceHolderName; }
        set { linkedPuzzlePieceHolderName = value; }
    }

    private string setPuzzlePieceHolderName;

    public string SetPuzzlePieceHolderName
    {
        get { return setPuzzlePieceHolderName; }
        set { setPuzzlePieceHolderName = value; }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        List<(GameObject, GameObject)> puzzlePiecesHolders = puzzleGameManager.PuzzlePiecesHolders;

        PuzzleGameManager.setGeneralColor(GetComponent<RectTransform>().GetChild(0).gameObject);

        foreach (var pph in puzzlePiecesHolders)
        {
            if (setPuzzlePieceHolderName == pph.Item2.name
             && pph.Item2.GetComponent<PuzzlePieceHolder>().SetPuzzlePieceName == this.gameObject.name)
            {
                GetComponent<RectTransform>().localScale = new Vector3(90f, 90f, 90f);

                pph.Item2.GetComponent<PuzzlePieceHolder>().SetPuzzlePieceName = string.Empty;

                break;
            }
        }

        if (setPuzzlePieceHolderName != string.Empty)
        {
            setPuzzlePieceHolderName = string.Empty;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        bool foundRighPuzzlePieceHolder = false;
        bool setPuzzle = false;
        List<(GameObject, GameObject)> puzzlePiecesHolders = puzzleGameManager.PuzzlePiecesHolders;

        foreach (var pph in puzzlePiecesHolders)
        {
            if (Vector2.Distance(pph.Item2.GetComponent<RectTransform>().anchoredPosition, GetComponent<RectTransform>().anchoredPosition) <= puzzleGameManager.distance
             && pph.Item2.GetComponent<PuzzlePieceHolder>().SetPuzzlePieceName == string.Empty)
            {
                if (pph.Item2.GetComponent<PuzzlePieceHolder>().LinkedPuzzlePieceName == gameObject.name)
                {
                    GetComponent<RectTransform>().anchoredPosition = pph.Item2.GetComponent<RectTransform>().anchoredPosition;
                    GetComponent<RectTransform>().localScale = new Vector3(135f, 135f, 135f);

                    pph.Item2.GetComponent<PuzzlePieceHolder>().SetPuzzlePieceName = this.gameObject.name;

                    setPuzzlePieceHolderName = pph.Item2.gameObject.name;

                    foundRighPuzzlePieceHolder = true;

                    break;
                }

                setPuzzle = true;
            }
        }

        if (!foundRighPuzzlePieceHolder && setPuzzle)
        {
            puzzleGameManager.playFailAudio();
        }

        if (setPuzzlePieceHolderName != string.Empty && foundRighPuzzlePieceHolder)
        {
            puzzleGameManager.playCardSetAudio();
        }
    }
}
