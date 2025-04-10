using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GLSilhouette : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]
    GoodLandscapeGameManager goodLandscapeGameManager;

    private string linkedSilhouetteHolderName;

    public string LinkedSilhouetteHolderName
    {
        get { return linkedSilhouetteHolderName; }
        set { linkedSilhouetteHolderName = value; }
    }

    private string setSilhouetteHolderName;

    public string SetSilhouetteHolderName
    {
        get { return setSilhouetteHolderName; }
        set { setSilhouetteHolderName = value; }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        List<(GoodLandscapeSilhouettes, GameObject)> silhouetteHolders = goodLandscapeGameManager.SilhouetteHolders;

        foreach (var sh in silhouetteHolders)
        {
            if (setSilhouetteHolderName == sh.Item2.gameObject.name
             && sh.Item2.GetComponent<GLSilhouetteHolder>().SetSilhouetteName == this.gameObject.name)
            {
                //sh.GetComponent<Outline>().effectColor = new Color32(30, 30, 30, 255);
                //sh.GetComponent<Image>().material.SetColor("_SolidOutline", new Color(0.1176471f, 0.1176471f, 0.1176471f, 1f));
                SilhouettesGameManager.setOutlineGeneralColor(sh.Item2.GetComponent<Image>().material);


                sh.Item2.GetComponent<GLSilhouetteHolder>().SetSilhouetteName = string.Empty;

                break;
            }
        }

        if (setSilhouetteHolderName != string.Empty)
        {
            setSilhouetteHolderName = string.Empty;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        bool foundRightSilhouetteHolder = false;
        bool setSilhouette = false;
        List<(GoodLandscapeSilhouettes, GameObject)> silhouetteHolders = goodLandscapeGameManager.SilhouetteHolders;

        foreach (var sh in silhouetteHolders)
        {
            if (Vector2.Distance(sh.Item2.GetComponent<RectTransform>().anchoredPosition, GetComponent<RectTransform>().anchoredPosition) <= goodLandscapeGameManager.distance
             && sh.Item2.GetComponent<GLSilhouetteHolder>().SetSilhouetteName == string.Empty)
            {
                if (sh.Item2.GetComponent<GLSilhouetteHolder>().LinkedSilhouetteName == gameObject.name)
                {
                    GetComponent<RectTransform>().anchoredPosition = sh.Item2.GetComponent<RectTransform>().anchoredPosition;

                    //sh.GetComponent<Outline>().effectColor = new Color32(65, 160, 234, 255);
                    //sh.GetComponent<Image>().material.SetColor("_SolidOutline", new Color(0.254902f, 0.627451f, 0.9176471f, 1f));
                    SilhouettesGameManager.setOutlineSetColor(sh.Item2.GetComponent<Image>().material);

                    sh.Item2.GetComponent<GLSilhouetteHolder>().SetSilhouetteName = this.gameObject.name;

                    setSilhouetteHolderName = sh.Item2.gameObject.name;

                    foundRightSilhouetteHolder = true;

                    break;
                }

                setSilhouette = true;
            }
        }

        if (!foundRightSilhouetteHolder && setSilhouette)
        {
            goodLandscapeGameManager.playFailAudio();
        }

        if (setSilhouetteHolderName != string.Empty && foundRightSilhouetteHolder)
        {
            goodLandscapeGameManager.playCardSetAudio();
        }
    }
}
