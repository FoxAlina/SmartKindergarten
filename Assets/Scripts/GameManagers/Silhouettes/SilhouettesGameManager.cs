using System.Collections;
using System.Collections.Generic;
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
    List<Vector2> initialSilhouetteHolderPositions;

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

        initInitialSilhouettePositions();
        instantiateSilhouettes();
    }

    public void refreshSilhouettes()
    {
        foreach (var sh in silhouetteHolders)
        {
            sh.GetComponent<Outline>().effectColor = new Color32(30, 30, 30, 255);
        }

        setSilhouettesToIntialPositions();

        instantiateSilhouettes();
    }

    private void initInitialSilhouettePositions()
    {
        initialSilhouettePositions = new List<Vector2>();

        foreach (var silhouette in silhouettes)
        {
            initialSilhouettePositions.Add(silhouette.GetComponent<RectTransform>().anchoredPosition);
        }

        initialSilhouetteHolderPositions = new List<Vector2>();

        foreach (var silhouetteHolder in silhouetteHolders)
        {
            initialSilhouetteHolderPositions.Add(silhouetteHolder.GetComponent<RectTransform>().anchoredPosition);
        }
    }

    private void setSilhouettesToIntialPositions()
    {
        for (int i = 0; i < initialSilhouettePositions.Count; i++)
        {
            silhouettes[i].GetComponent<RectTransform>().anchoredPosition = initialSilhouettePositions[i];
        }

        for (int i = 0; i < initialSilhouetteHolderPositions.Count; i++)
        {
            silhouetteHolders[i].GetComponent<RectTransform>().anchoredPosition = initialSilhouetteHolderPositions[i];
        }
    }

    private void instantiateSilhouettes()
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
            silhouetteHolders[i].GetComponent<Image>().color = new Color32(30, 30, 30, 255);
            silhouetteHolders[i].GetComponent<Image>().SetNativeSize();
            rectTransformLocal = silhouetteHolders[i].GetComponent<RectTransform>();
            rectTransformLocal.sizeDelta = new Vector2(rectTransformLocal.sizeDelta.x / spriteManager.SilhouettesScaleFactor,
                                                       rectTransformLocal.sizeDelta.y / spriteManager.SilhouettesScaleFactor);
            silhouetteHolders[i].GetComponent<SilhouetteHolder>().LinkedSilhouetteName = silhouettes[i].gameObject.name;
        }
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
}
