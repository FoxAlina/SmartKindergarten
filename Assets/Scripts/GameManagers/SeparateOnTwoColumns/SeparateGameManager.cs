using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SeparateGameManager : BasicGameManager
{
    [SerializeField]
    public float distance = 10.0f;

    [SerializeField]
    List<GameObject> goodGroup;
    [SerializeField]
    List<GameObject> badGroup;

    [SerializeField]
    List<GameObject> cards;

    List<Vector2> initialCardPositions;

    public List<GameObject> Cards
    {
        get { return cards; }
        set { cards = value; }
    }

    public List<GameObject> GoodBadCards()
    {
        List<GameObject> ret = new List<GameObject>();

        ret.AddRange(goodGroup);
        ret.AddRange(badGroup);

        return ret;
    }

    private void Start()
    {
        GameOver = false;
        spriteManager = GetComponent<SpriteManager>();
        dataManager = GetComponent<DataManager>();

        initRounds();

        instantiateCards();
        initInitialCardPositions();
    }

    private void initInitialCardPositions()
    {
        initialCardPositions = new List<Vector2>();

        foreach (var card in cards)
        {
            initialCardPositions.Add(card.GetComponent<RectTransform>().anchoredPosition);
        }
    }

    private void instantiateCards()
    {
        Sprite sprite;
        int i = 1;
        bool isGood;
        int amountOfEachGroup = cards.Count / 2;

        //cards = cards.OrderBy(x => Random.value).ToList(); // if shuffle then need to save names to reset initial position on refresh/restart

        foreach (var card in cards)
        {
            isGood = (i <= amountOfEachGroup);

            sprite = spriteManager.getRandomCardSprite(isGood);

            card.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
            card.GetComponent<SeparateCard>().IsGoodCard = isGood;

            i++;
        }
    }

    public void setCardHolderSprite(GameObject _card, CardSetSuccessFail _spriteType)
    {
        switch (_spriteType)
        {
            case CardSetSuccessFail.Holder:
                _card.GetComponent<Image>().sprite = spriteManager.CardHolder;
                break;
            case CardSetSuccessFail.Set:
                _card.GetComponent<Image>().sprite = spriteManager.CardSet;
                break;
            case CardSetSuccessFail.Success:
                _card.GetComponent<Image>().sprite = spriteManager.CardSuccess;
                break;
            case CardSetSuccessFail.Fail:
                _card.GetComponent<Image>().sprite = spriteManager.CardFail;
                break;
        }
    }

    public void checkAnswers()
    {
        bool isFail = false;

        foreach (var card in goodGroup)
        {
            if (card.GetComponent<CardHolder>().CardSet != string.Empty
             && GameObject.Find(card.GetComponent<CardHolder>().CardSet).GetComponent<SeparateCard>().IsGoodCard == true)
            {
                this.setCardHolderSprite(card, CardSetSuccessFail.Success);
            }
            else
            {
                this.setCardHolderSprite(card, CardSetSuccessFail.Fail);

                isFail = true;
            }
        }

        foreach (var card in badGroup)
        {
            if (card.GetComponent<CardHolder>().CardSet != string.Empty
             && GameObject.Find(card.GetComponent<CardHolder>().CardSet).GetComponent<SeparateCard>().IsGoodCard == false)
            {
                this.setCardHolderSprite(card, CardSetSuccessFail.Success);
            }
            else
            {
                this.setCardHolderSprite(card, CardSetSuccessFail.Fail);

                isFail = true;
            }
        }

        if (!isFail)
        {
            audioSource.clip = successAudio;
            audioSource.Play();

            countRounds();

            setGameOver();
        }
        else
        {
            audioSource.clip = failAudioClip;
            audioSource.Play();
        }
    }

    private void setGameOver()
    {
        foreach (var card in cards)
        {
            card.GetComponent<DragAndDrop>().enabled = false;
        }

        checkButton.SetActive(false);
        refreshButton.SetActive(false);
        resetButton.SetActive(!GameOver);
    }

    public void resetGame()
    {
        if (!GameOver)
        {
            foreach (var card in cards)
            {
                card.GetComponent<DragAndDrop>().enabled = true;
            }

            refreshCards();

            checkButton.SetActive(true);
            refreshButton.SetActive(true);
            resetButton.SetActive(false);
        }
    }

    public void refreshCards()
    {
        if (!GameOver)
        {
            instantiateCards();

            for (int i = 0; i < cards.Count; i++)
            {
                cards[i].GetComponent<RectTransform>().anchoredPosition = initialCardPositions[i];
            }

            List<GameObject> cardHolders = GoodBadCards();

            foreach (var cardHolder in cardHolders)
            {
                this.setCardHolderSprite(cardHolder, CardSetSuccessFail.Holder);

                cardHolder.GetComponent<CardHolder>().CardSet = string.Empty;
            }
        }
    }

    public void playCardSetAudio()
    {
        audioSource.clip = setCardAudio;
        audioSource.Play();
    }
}
