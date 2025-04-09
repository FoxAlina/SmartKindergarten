using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Silhouette : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]
    SilhouettesGameManager silhouettesGameManager;

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
        List<GameObject> silhouetteHolders = silhouettesGameManager.SilhouetteHolders;

        foreach (var sh in silhouetteHolders)
        {
            if (setSilhouetteHolderName == sh.gameObject.name
             && sh.GetComponent<SilhouetteHolder>().SetSilhouetteName == this.gameObject.name)
            {
                sh.GetComponent<Outline>().effectColor = new Color32(30, 30, 30, 255);

                sh.GetComponent<SilhouetteHolder>().SetSilhouetteName = string.Empty;

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
        List<GameObject> silhouetteHolders = silhouettesGameManager.SilhouetteHolders;

        foreach (var sh in silhouetteHolders)
        {
            if (Vector2.Distance(sh.GetComponent<RectTransform>().anchoredPosition, GetComponent<RectTransform>().anchoredPosition) <= silhouettesGameManager.distance
             && sh.GetComponent<SilhouetteHolder>().SetSilhouetteName == string.Empty)
            {
                if (sh.GetComponent<SilhouetteHolder>().LinkedSilhouetteName == gameObject.name)
                {
                    GetComponent<RectTransform>().anchoredPosition = sh.GetComponent<RectTransform>().anchoredPosition;

                    sh.GetComponent<Outline>().effectColor = new Color32(65, 160, 234, 255);

                    sh.GetComponent<SilhouetteHolder>().SetSilhouetteName = this.gameObject.name;

                    setSilhouetteHolderName = sh.gameObject.name;

                    foundRightSilhouetteHolder = true;

                    break;
                }

                setSilhouette = true;
            }
        }

        if (!foundRightSilhouetteHolder && setSilhouette)
        {
            silhouettesGameManager.playFailAudio();
        }

        if (setSilhouetteHolderName != string.Empty && foundRightSilhouetteHolder)
        {
            silhouettesGameManager.playCardSetAudio();
        }
    }
}
