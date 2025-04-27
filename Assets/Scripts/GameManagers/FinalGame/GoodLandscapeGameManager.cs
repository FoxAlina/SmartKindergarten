using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GoodLandscapeGameManager : BasicGameManager
{
    [SerializeField]
    GameObject background;

    [SerializeField]
    List<GameObject> sunSilhouettes;
    [SerializeField]
    List<GameObject> sunSilhouetteHolders;
    [SerializeField]
    List<GameObject> highSilhouettes;
    [SerializeField]
    List<GameObject> highSilhouetteHolders;
    [SerializeField]
    List<GameObject> middleSilhouettes;
    [SerializeField]
    List<GameObject> middleSilhouetteHolders;
    [SerializeField]
    List<GameObject> lowSilhouettes;
    [SerializeField]
    List<GameObject> lowSilhouetteHolders;
    [SerializeField]
    public float distance = 10.0f;

    int sunNumber;
    int highNumber;
    int middleNumber;
    int lowNumber;
    List<Vector2> initialSilhouettePositions;
    List<(GoodLandscapeSilhouettes, GameObject)> silhouettes;
    List<(GoodLandscapeSilhouettes, GameObject)> silhouetteHolders;

    public List<(GoodLandscapeSilhouettes, GameObject)> SilhouetteHolders
    {
        get { return silhouetteHolders; }
    }

    public List<(GoodLandscapeSilhouettes, GameObject)> Silhouettes
    {
        get { return silhouettes; }
    }

    void Start()
    {
        GameOver = false;
        spriteManager = GetComponent<SpriteManager>();
        dataManager = GetComponent<DataManager>();

        background.GetComponent<Image>().sprite = spriteManager.getRandomGLBackgroundSprite();

        this.playExplanationAudio();

        initCongratsCharacter();

        initRounds();

        initializeSilhouettesLists();
        initializeSilhouetteHoldersLists();
        initInitialSilhouettePositions();
        shuffleSilhouettes();

        cleanOutlines();
        instantiateSilhouettes();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    public void refreshSilhouettes()
    {
        cleanOutlines();
        shuffleSilhouettes();
        instantiateSilhouettes();
        refreshSetSilhouettes();
    }
    private void shuffleSilhouettes()
    {
        initialSilhouettePositions = initialSilhouettePositions.OrderBy(x => Random.value).ToList();
        setSilhouettesToIntialPositions();
    }

    private void initializeSilhouettesLists()
    {
        silhouettes = new List<(GoodLandscapeSilhouettes, GameObject)>();

        foreach (var silhouette in sunSilhouettes)
        {
            silhouettes.Add((GoodLandscapeSilhouettes.Sun, silhouette));
        }

        foreach (var silhouette in highSilhouettes)
        {
            silhouettes.Add((GoodLandscapeSilhouettes.High, silhouette));
        }

        foreach (var silhouette in middleSilhouettes)
        {
            silhouettes.Add((GoodLandscapeSilhouettes.Middle, silhouette));
        }

        foreach (var silhouette in lowSilhouettes)
        {
            silhouettes.Add((GoodLandscapeSilhouettes.Low, silhouette));
        }
    }

    private void initializeSilhouetteHoldersLists()
    {
        silhouetteHolders = new List<(GoodLandscapeSilhouettes, GameObject)>();

        foreach (var sh in sunSilhouetteHolders)
        {
            silhouetteHolders.Add((GoodLandscapeSilhouettes.Sun, sh));
        }

        foreach (var sh in highSilhouetteHolders)
        {
            silhouetteHolders.Add((GoodLandscapeSilhouettes.High, sh));
        }

        foreach (var sh in middleSilhouetteHolders)
        {
            silhouetteHolders.Add((GoodLandscapeSilhouettes.Middle, sh));
        }

        foreach (var sh in lowSilhouetteHolders)
        {
            silhouetteHolders.Add((GoodLandscapeSilhouettes.Low, sh));
        }
    }

    protected void initInitialSilhouettePositions()
    {
        initialSilhouettePositions = new List<Vector2>();

        sunNumber = sunSilhouettes.Count;
        highNumber = highSilhouettes.Count;
        middleNumber = middleSilhouettes.Count;
        lowNumber = lowSilhouettes.Count;

        foreach (var silhouette in sunSilhouettes)
        {
            initialSilhouettePositions.Add(silhouette.GetComponent<RectTransform>().anchoredPosition);
        }

        foreach (var silhouette in highSilhouettes)
        {
            initialSilhouettePositions.Add(silhouette.GetComponent<RectTransform>().anchoredPosition);
        }

        foreach (var silhouette in middleSilhouettes)
        {
            initialSilhouettePositions.Add(silhouette.GetComponent<RectTransform>().anchoredPosition);
        }

        foreach (var silhouette in lowSilhouettes)
        {
            initialSilhouettePositions.Add(silhouette.GetComponent<RectTransform>().anchoredPosition);
        }
    }

    private void setSilhouettesToIntialPositions()
    {
        GoodLandscapeSilhouettes silhouetteType = GoodLandscapeSilhouettes.Sun;
        int sunSilhouettesCounter = 0;
        int highSilhouettesCounter = 0;
        int middleSilhouettesCounter = 0;
        int lowSilhouettesCounter = 0;

        for (int i = 0; i < initialSilhouettePositions.Count; i++)
        {
            switch (silhouetteType)
            {
                case GoodLandscapeSilhouettes.Sun:
                    sunSilhouettes[sunSilhouettesCounter].GetComponent<RectTransform>().anchoredPosition = initialSilhouettePositions[i];
                    sunSilhouettesCounter++;
                    break;
                case GoodLandscapeSilhouettes.High:
                    highSilhouettes[highSilhouettesCounter].GetComponent<RectTransform>().anchoredPosition = initialSilhouettePositions[i];
                    highSilhouettesCounter++;
                    break;
                case GoodLandscapeSilhouettes.Middle:
                    middleSilhouettes[middleSilhouettesCounter].GetComponent<RectTransform>().anchoredPosition = initialSilhouettePositions[i];
                    middleSilhouettesCounter++;
                    break;
                case GoodLandscapeSilhouettes.Low:
                    lowSilhouettes[lowSilhouettesCounter].GetComponent<RectTransform>().anchoredPosition = initialSilhouettePositions[i];
                    lowSilhouettesCounter++;
                    break;
            }

            if (sunSilhouettesCounter == sunNumber
                && highSilhouettesCounter == 0)
            {
                silhouetteType = GoodLandscapeSilhouettes.High;
            }

            if (highSilhouettesCounter == highNumber
                && middleSilhouettesCounter == 0)
            {
                silhouetteType = GoodLandscapeSilhouettes.Middle;
            }


            if (middleSilhouettesCounter == middleNumber
                && lowSilhouettesCounter == 0)
            {
                silhouetteType = GoodLandscapeSilhouettes.Low;
            }
        }
    }

    protected void instantiateSilhouettes()
    {
        Sprite sprite;
        RectTransform rectTransformLocal;

        for (int i = 0; i < silhouettes.Count; i++)
        {
            sprite = spriteManager.getRandomGLSilhouetteSprite(silhouettes[i].Item1);

            silhouettes[i].Item2.GetComponent<Image>().sprite = sprite;
            silhouettes[i].Item2.GetComponent<Image>().SetNativeSize();
            rectTransformLocal = silhouettes[i].Item2.GetComponent<RectTransform>();
            rectTransformLocal.sizeDelta = new Vector2(rectTransformLocal.sizeDelta.x / spriteManager.SilhouettesScaleFactor, rectTransformLocal.sizeDelta.y / spriteManager.SilhouettesScaleFactor);
            silhouettes[i].Item2.GetComponent<GLSilhouette>().LinkedSilhouetteHolderName = silhouetteHolders[i].Item2.gameObject.name;

            silhouetteHolders[i].Item2.GetComponent<Image>().sprite = sprite;
            silhouetteHolders[i].Item2.GetComponent<Image>().color = new Color32(70, 70, 70, 255);
            silhouetteHolders[i].Item2.GetComponent<Image>().SetNativeSize();
            rectTransformLocal = silhouetteHolders[i].Item2.GetComponent<RectTransform>();
            rectTransformLocal.sizeDelta = new Vector2(rectTransformLocal.sizeDelta.x / spriteManager.SilhouettesScaleFactor,
                                                       rectTransformLocal.sizeDelta.y / spriteManager.SilhouettesScaleFactor);
            float deltaX = 0.82f
                + ((SilhouetteHolders[i].Item2.GetComponent<Image>().material.GetFloat("_Thickness")) / SilhouetteHolders[i].Item2.GetComponent<RectTransform>().sizeDelta.x);
            float deltaY = 0.85f
                + ((SilhouetteHolders[i].Item2.GetComponent<Image>().material.GetFloat("_Thickness")) / SilhouetteHolders[i].Item2.GetComponent<RectTransform>().sizeDelta.y);
            rectTransformLocal.sizeDelta = new Vector2(rectTransformLocal.sizeDelta.x * deltaX,
                                                       rectTransformLocal.sizeDelta.y * deltaY);
            silhouetteHolders[i].Item2.GetComponent<GLSilhouetteHolder>().LinkedSilhouetteName = silhouettes[i].Item2.gameObject.name;
        }
    }

    private void refreshSetSilhouettes()
    {
        foreach (var sl in silhouettes)
        {
            sl.Item2.GetComponent<GLSilhouette>().SetSilhouetteHolderName = string.Empty;
        }

        foreach (var sh in silhouetteHolders)
        {
            sh.Item2.GetComponent<GLSilhouetteHolder>().SetSilhouetteName = string.Empty;
        }
    }

    protected void cleanOutlines()
    {
        foreach (var sh in silhouetteHolders)
        {
            setOutlineGeneralColor(sh.Item2.GetComponent<Image>().material);
        }
    }

    public void checkAnswers()
    {
        bool success = true;

        foreach (var sh in silhouetteHolders)
        {
            if (sh.Item2.GetComponent<GLSilhouetteHolder>().SetSilhouetteName == sh.Item2.GetComponent<GLSilhouetteHolder>().LinkedSilhouetteName)
            {
                setOutlineSuccessColor(sh.Item2.GetComponent<Image>().material);
            }
            else
            {
                setOutlineFailColor(sh.Item2.GetComponent<Image>().material);

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
        refreshSilhouettes();

        checkButton.SetActive(true);
        refreshButton.SetActive(true);
        resetButton.SetActive(false);
    }

    public static void setOutlineSetColor(Material _material)
    {
        //sh.GetComponent<Outline>().effectColor = new Color32(65, 160, 234, 255);
        _material.SetColor("_SolidOutline", new Color(0.254902f, 0.627451f, 0.9176471f, 1f));
    }

    public static void setOutlineGeneralColor(Material _material)
    {
        //sh.GetComponent<Outline>().effectColor = new Color32(30, 30, 30, 255);
        _material.SetColor("_SolidOutline", new Color(0.1176471f, 0.1176471f, 0.1176471f, 1f));
    }

    public static void setOutlineSuccessColor(Material _material)
    {
        //sh.GetComponent<Outline>().effectColor = new Color32(65, 160, 234, 255);
        _material.SetColor("_SolidOutline", new Color(0.2644187f, 0.9176471f, 0.2549019f, 1f));
    }

    public static void setOutlineFailColor(Material _material)
    {
        //sh.GetComponent<Outline>().effectColor = new Color32(65, 160, 234, 255);
        _material.SetColor("_SolidOutline", new Color(0.8584906f, 0.2308206f, 0.2478598f, 1f));
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
