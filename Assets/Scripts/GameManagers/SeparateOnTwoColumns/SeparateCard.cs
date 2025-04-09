using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SeparateCard : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    private bool isGoodCard;

    [SerializeField]
    private SeparateGameManager separateGameManager;

    private RectTransform rectTransform;
    private string cardHolder;

    public bool IsGoodCard
    {
        get { return isGoodCard; }
        set { isGoodCard = value; }
    }

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        List<GameObject> cards = separateGameManager.GoodBadCards();

        foreach (var card in cards)
        {
            if (Vector2.Distance(card.GetComponent<RectTransform>().anchoredPosition, rectTransform.anchoredPosition) <= separateGameManager.distance
             && card.GetComponent<CardHolder>().CardSet == string.Empty)
            {
                rectTransform.anchoredPosition = card.GetComponent<RectTransform>().anchoredPosition;

                separateGameManager.setCardHolderSprite(card, CardSetSuccessFail.Set);

                card.GetComponent<CardHolder>().CardSet = this.gameObject.name;

                cardHolder = card.gameObject.name;

                break;
            }
        }

        if (cardHolder != string.Empty)
        {
            separateGameManager.playCardSetAudio();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        List<GameObject> cards = separateGameManager.GoodBadCards();

        foreach (var card in cards)
        {
            if (cardHolder == card.gameObject.name
             && card.GetComponent<CardHolder>().CardSet == this.gameObject.name)
            {
                separateGameManager.setCardHolderSprite(card, CardSetSuccessFail.Holder);

                card.GetComponent<CardHolder>().CardSet = string.Empty;

                break;
            }
        }

        if (cardHolder != string.Empty)
        {
            cardHolder = string.Empty;
        }
    }
}
