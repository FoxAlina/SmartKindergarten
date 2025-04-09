using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FindExtraCard : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    FindExtraGameManager findExtraGameManager;

    private bool isExtra;
    private bool isChosen;

    public bool IsExtra
    {
        get { return isExtra; }
        set { isExtra = value; }
    }

    public bool IsChosen
    {
        get { return isChosen; }
        set { isChosen = value; }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        findExtraGameManager.unsetSetCard();
        findExtraGameManager.setCardHolderSprite(gameObject, CardSetSuccessFail.Set);
        findExtraGameManager.playSound(Sounds.Set);

        isChosen = true;
    }
}
