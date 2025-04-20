using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SilhouettesGameManager : BasicGameManager
{
    [SerializeField]
    List<GameObject> silhouettes;
    [SerializeField]
    List<GameObject> silhouetteHolders;
    [SerializeField]
    public float distance = 10.0f;

    List<Vector2> initialSilhouettePositions;

    public List<GameObject> SilhouetteHolders
    {
        get { return silhouetteHolders; }
    }

    public List<GameObject> Silhouettes
    {
        get { return silhouettes; }
    }

    void Start()
    {
        GameOver = false;
        spriteManager = GetComponent<SpriteManager>();
        dataManager = GetComponent<DataManager>();

        initCongratsCharacter();

        initRounds();

        cleanOutlines();
        initInitialSilhouettePositions();
        shuffleSilhouettes();
        instantiateSilhouettes();
    }

    public void refreshSilhouettes()
    {
        cleanOutlines();
        shuffleSilhouettes();
        instantiateSilhouettes();
        refreshSetSilhouettes();
    }

    protected void initInitialSilhouettePositions()
    {
        initialSilhouettePositions = new List<Vector2>();

        foreach (var silhouette in silhouettes)
        {
            initialSilhouettePositions.Add(silhouette.GetComponent<RectTransform>().anchoredPosition);
        }
    }

    private void shuffleSilhouettes()
    {
        initialSilhouettePositions = initialSilhouettePositions.OrderBy(x => Random.value).ToList();
        setSilhouettesToIntialPositions();
    }

    private void setSilhouettesToIntialPositions()
    {
        for (int i = 0; i < initialSilhouettePositions.Count; i++)
        {
            silhouettes[i].GetComponent<RectTransform>().anchoredPosition = initialSilhouettePositions[i];
        }
    }

    protected void instantiateSilhouettes()
    {
        Sprite sprite;
        RectTransform rectTransformLocal;

        for (int i = 0; i < silhouettes.Count; i++)
        {
            sprite = spriteManager.getRandomSilhouetteSprite();

            silhouettes[i].GetComponent<Image>().sprite = sprite;
            silhouettes[i].GetComponent<Image>().SetNativeSize();
            rectTransformLocal = silhouettes[i].GetComponent<RectTransform>();
            rectTransformLocal.sizeDelta = new Vector2(rectTransformLocal.sizeDelta.x / spriteManager.SilhouettesScaleFactor, rectTransformLocal.sizeDelta.y / spriteManager.SilhouettesScaleFactor);
            silhouettes[i].GetComponent<Silhouette>().LinkedSilhouetteHolderName = silhouetteHolders[i].gameObject.name;

            silhouetteHolders[i].GetComponent<Image>().sprite = sprite;
            silhouetteHolders[i].GetComponent<Image>().color = new Color32(70, 70, 70, 255);
            silhouetteHolders[i].GetComponent<Image>().SetNativeSize();
            rectTransformLocal = silhouetteHolders[i].GetComponent<RectTransform>();
            rectTransformLocal.sizeDelta = new Vector2(rectTransformLocal.sizeDelta.x / spriteManager.SilhouettesScaleFactor,
                                                       rectTransformLocal.sizeDelta.y / spriteManager.SilhouettesScaleFactor);
            float deltaX = 0.82f
                + ((SilhouetteHolders[i].GetComponent<Image>().material.GetFloat("_Thickness")) / SilhouetteHolders[i].GetComponent<RectTransform>().sizeDelta.x);
            float deltaY = 0.89f
                + ((SilhouetteHolders[i].GetComponent<Image>().material.GetFloat("_Thickness")) / SilhouetteHolders[i].GetComponent<RectTransform>().sizeDelta.y);
            rectTransformLocal.sizeDelta = new Vector2(rectTransformLocal.sizeDelta.x * deltaX,
                                                       rectTransformLocal.sizeDelta.y * deltaY);
            silhouetteHolders[i].GetComponent<SilhouetteHolder>().LinkedSilhouetteName = silhouettes[i].gameObject.name;
        }
    }

    private void refreshSetSilhouettes()
    {
        foreach (var sl in silhouettes)
        {
            sl.GetComponent<Silhouette>().SetSilhouetteHolderName = string.Empty;
        }

        foreach (var sh in silhouetteHolders)
        {
            sh.GetComponent<SilhouetteHolder>().SetSilhouetteName = string.Empty;
        }
    }

    protected void cleanOutlines()
    {
        foreach (var sh in silhouetteHolders)
        {
            setOutlineGeneralColor(sh.GetComponent<Image>().material);
        }
    }

    public void checkAnswers()
    {
        bool success = true;

        foreach (var sh in silhouetteHolders)
        {
            if (sh.GetComponent<SilhouetteHolder>().SetSilhouetteName == sh.GetComponent<SilhouetteHolder>().LinkedSilhouetteName)
            {
                setOutlineSuccessColor(sh.GetComponent<Image>().material);
            }
            else
            {
                setOutlineFailColor(sh.GetComponent<Image>().material);

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
