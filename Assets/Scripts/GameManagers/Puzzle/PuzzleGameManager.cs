using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleGameManager : BasicGameManager
{
    [SerializeField]
    List<GameObject> puzzlePieces;
    [SerializeField]
    List<GameObject> puzzleHolders;
    [SerializeField]
    public float distance = 10.0f;

    List<(GameObject, GameObject)> puzzlePiecesHolders;
    List<Vector2> initialPuzzlePiecesPositions;

    public List<(GameObject, GameObject)> PuzzlePiecesHolders
    {
        get { return puzzlePiecesHolders; }
    }

    void Start()
    {
        GameOver = false;
        spriteManager = GetComponent<SpriteManager>();
        dataManager = GetComponent<DataManager>();

        this.playExplanationAudio();

        initCongratsCharacter();

        initPuzzlePiecesHolders();

        initRounds();

        cleanSuccessFailColor();
        initInitialPuzzlePositions();
        shufflePuzzlePieces();
        instantiatePuzzle();
    }

    public void refreshPuzzle()
    {
        cleanSuccessFailColor();
        shufflePuzzlePieces();
        instantiatePuzzle();
        refreshSetSilhouettes();
    }

    private void shufflePuzzlePieces()
    {
        initialPuzzlePiecesPositions = initialPuzzlePiecesPositions.OrderBy(x => Random.value).ToList();

        setPuzzlePiecesToIntialPositions();
    }

    private void initPuzzlePiecesHolders()
    {
        puzzlePiecesHolders = new List<(GameObject, GameObject)>();

        for (int i = 0; i < puzzlePieces.Count; i++)
        {
            puzzlePiecesHolders.Add((puzzlePieces[i], puzzleHolders[i]));
        }
    }

    protected void initInitialPuzzlePositions()
    {
        initialPuzzlePiecesPositions = new List<Vector2>();

        foreach (var pp in puzzlePiecesHolders)
        {
            initialPuzzlePiecesPositions.Add(pp.Item1.GetComponent<RectTransform>().anchoredPosition);
        }
    }

    private void setPuzzlePiecesToIntialPositions()
    {
        for (int i = 0; i < initialPuzzlePiecesPositions.Count; i++)
        {
            puzzlePiecesHolders[i].Item1.GetComponent<RectTransform>().anchoredPosition = initialPuzzlePiecesPositions[i];
            puzzlePiecesHolders[i].Item1.GetComponent<RectTransform>().localScale = new Vector3(90f, 90f, 90f);
        }
    }

    protected void instantiatePuzzle()
    {
        Sprite sprite = spriteManager.getRandomPuzzle();

        for (int i = 0; i < puzzlePiecesHolders.Count; i++)
        {
            puzzlePiecesHolders[i].Item1.GetComponent<RectTransform>().GetChild(0).GetComponent<Image>().sprite = sprite;
            puzzlePiecesHolders[i].Item1.GetComponent<PuzzlePiece>().LinkedPuzzlePieceHolderName = puzzlePiecesHolders[i].Item2.gameObject.name;

            puzzlePiecesHolders[i].Item2.GetComponent<RectTransform>().GetChild(0).GetComponent<Image>().sprite = sprite;
            puzzlePiecesHolders[i].Item2.GetComponent<PuzzlePieceHolder>().LinkedPuzzlePieceName = puzzlePiecesHolders[i].Item1.gameObject.name;
        }
    }

    private void refreshSetSilhouettes()
    {
        foreach (var pph in puzzlePiecesHolders)
        {
            pph.Item1.GetComponent<PuzzlePiece>().SetPuzzlePieceHolderName = string.Empty;
            pph.Item2.GetComponent<PuzzlePieceHolder>().SetPuzzlePieceName = string.Empty;
        }
    }

    protected void cleanSuccessFailColor()
    {
        foreach (var pph in puzzlePiecesHolders)
        {
            setGeneralColor(pph.Item1.GetComponent<RectTransform>().GetChild(0).gameObject);
        }
    }

    public void checkAnswers()
    {
        bool success = true;

        foreach (var pph in puzzlePiecesHolders)
        {
            if (pph.Item2.GetComponent<PuzzlePieceHolder>().SetPuzzlePieceName == pph.Item2.GetComponent<PuzzlePieceHolder>().LinkedPuzzlePieceName)
            {
                StartCoroutine(setSuccessFailColor(pph.Item1.GetComponent<RectTransform>().GetChild(0).gameObject, true));
            }
            else
            {
                StartCoroutine(setSuccessFailColor(pph.Item1.GetComponent<RectTransform>().GetChild(0).gameObject, false));

                success = false;
            }
        }

        if (success)
        {
            countRounds();

            playSuccessAudio();

            checkButton.SetActive(false);
            refreshButton.SetActive(false);
            resetButton.SetActive(!GameOver);

            StartCoroutine(setHappySadCharacter(true));
        }
        else
        {
            playFailAudio();

            StartCoroutine(setHappySadCharacter());
        }
    }

    public void restart()
    {
        refreshPuzzle();

        checkButton.SetActive(true);
        refreshButton.SetActive(true);
        resetButton.SetActive(false);
    }
    protected IEnumerator setSuccessFailColor(GameObject _gameObject, bool _success = false)
    {
        if (_success) setSuccessColor(_gameObject);
        else setFailColor(_gameObject);

        yield return new WaitForSeconds(happySeconds);

        setGeneralColor(_gameObject);
    }

    public static void setGeneralColor(GameObject _gameObject)
    {
        _gameObject.GetComponent<Image>().color = new Color32(255, 255, 225, 255);
    }

    public static void setSuccessColor(GameObject _gameObject)
    {
        _gameObject.GetComponent<Image>().color = new Color32(0, 255, 0, 255);
    }

    public static void setFailColor(GameObject _gameObject)
    {
        _gameObject.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
    }

    public void playCardSetAudio()
    {
        audioSource.clip = setCardAudio;
        audioSource.Play();
    }

    public void playFailAudio()
    {
        audioSource.clip = failAudioClip;
        audioSource.Play();
    }

    public void playSuccessAudio()
    {
        audioSource.clip = successAudio;
        audioSource.Play();
    }
}
