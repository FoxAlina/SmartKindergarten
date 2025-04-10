using System.Collections;
using System.Collections.Generic;
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

    List<(GoodLandscapeSilhouettes, Vector2)> initialSilhouettePositions;
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

        initRounds();

        initializeSilhouettesLists();
        initializeSilhouetteHoldersLists();
        initInitialSilhouettePositions();

        cleanOutlines();
        instantiateSilhouettes();
    }

    public void refreshSilhouettes()
    {
        cleanOutlines();
        setSilhouettesToIntialPositions();
        instantiateSilhouettes();
        refreshSetSilhouettes();
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
        initialSilhouettePositions = new List<(GoodLandscapeSilhouettes, Vector2)>();

        foreach (var silhouette in sunSilhouettes)
        {
            initialSilhouettePositions.Add((GoodLandscapeSilhouettes.Sun, silhouette.GetComponent<RectTransform>().anchoredPosition));
        }

        foreach (var silhouette in highSilhouettes)
        {
            initialSilhouettePositions.Add((GoodLandscapeSilhouettes.High, silhouette.GetComponent<RectTransform>().anchoredPosition));
        }

        foreach (var silhouette in middleSilhouettes)
        {
            initialSilhouettePositions.Add((GoodLandscapeSilhouettes.Middle, silhouette.GetComponent<RectTransform>().anchoredPosition));
        }

        foreach (var silhouette in lowSilhouettes)
        {
            initialSilhouettePositions.Add((GoodLandscapeSilhouettes.Low, silhouette.GetComponent<RectTransform>().anchoredPosition));
        }
    }

    private void setSilhouettesToIntialPositions()
    {
        int sunSilhouettesCounter = 0;
        int highSilhouettesCounter = 0;
        int middleSilhouettesCounter = 0;
        int lowSilhouettesCounter = 0;

        for (int i = 0; i < initialSilhouettePositions.Count; i++)
        {
            switch (initialSilhouettePositions[i].Item1)
            {
                case GoodLandscapeSilhouettes.Sun:
                    sunSilhouettes[sunSilhouettesCounter].GetComponent<RectTransform>().anchoredPosition = initialSilhouettePositions[i].Item2;
                    sunSilhouettesCounter++;
                    break;
                case GoodLandscapeSilhouettes.High:
                    highSilhouettes[highSilhouettesCounter].GetComponent<RectTransform>().anchoredPosition = initialSilhouettePositions[i].Item2;
                    highSilhouettesCounter++;
                    break;
                case GoodLandscapeSilhouettes.Middle:
                    middleSilhouettes[middleSilhouettesCounter].GetComponent<RectTransform>().anchoredPosition = initialSilhouettePositions[i].Item2;
                    middleSilhouettesCounter++;
                    break;
                case GoodLandscapeSilhouettes.Low:
                    lowSilhouettes[lowSilhouettesCounter].GetComponent<RectTransform>().anchoredPosition = initialSilhouettePositions[i].Item2;
                    lowSilhouettesCounter++;
                    break;
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
            silhouetteHolders[i].Item2.GetComponent<Image>().color = new Color32(30, 30, 30, 255);
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
        }
        else
        {
            playFailAudio();
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
