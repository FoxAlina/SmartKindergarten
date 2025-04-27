using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FindExtraGameManager : BasicGameManager
{
    [SerializeField]
    List<GameObject> cards;

    public List<GameObject> Cards
    {
        get { return cards; }
        set { cards = value; }
    }

    private void Start()
    {
        GameOver = false;
        spriteManager = GetComponent<SpriteManager>();
        dataManager = GetComponent<DataManager>();

        this.playExplanationAudio();

        initCongratsCharacter();

        initRounds();

        instantiateCards();
    }

    private void instantiateCards()
    {
        Sprite sprite;
        int i = 1;
        int random = Random.Range(0, 150);
        int remainder = random % 2;
        bool isGood, isExtra;
        int amountOfEachGroup = cards.Count - 1;

        cards = cards.OrderBy(x => Random.value).ToList();

        foreach (var card in cards)
        {
            isExtra = (i > amountOfEachGroup);
            isGood = isExtra ^ (remainder > 0);

            sprite = spriteManager.getRandomCardSprite(isGood);

            card.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
            card.GetComponent<FindExtraCard>().IsExtra = isExtra;

            i++;
        }
    }

    public void refreshCards()
    {
        instantiateCards();
    }

    public void checkAnswers()
    {
        bool isSuccess = false;

        foreach(var card in cards)
        {
            if (card.GetComponent<FindExtraCard>().IsChosen && card.GetComponent<FindExtraCard>().IsExtra)
            {
                success();

                isSuccess = true;

                break;
            }
        }

        if (!isSuccess)
        {
            fail();
        }
    }

    private void success()
    {
        playSound(Sounds.Success);

        countRounds();

        foreach (var card in cards)
        {
            if (card.GetComponent<FindExtraCard>().IsChosen == true)
            {
                setCardHolderSprite(card, CardSetSuccessFail.Success);
            }
        }

        setEnableCards(false);

        checkButton.SetActive(false);
        refreshButton.SetActive(false);
        resetButton.SetActive(!GameOver);

        StartCoroutine(setHappySadCharacter(true));
    }

    private void fail()
    {
        playSound(Sounds.Fail);

        foreach (var card in cards)
        {
            if (card.GetComponent<FindExtraCard>().IsChosen == true)
            {
                setCardHolderSprite(card, CardSetSuccessFail.Fail);
            }
        }

        StartCoroutine(setHappySadCharacter());
    }

    private void setEnableCards(bool _enable)
    {
        foreach (var card in cards)
        {
            card.GetComponent<FindExtraCard>().enabled = _enable;
        }
    }

    public void restartGame()
    {
        GameOver = false;

        setEnableCards(true);
        refreshCards();
        unsetSetCard();

        checkButton.SetActive(true);
        refreshButton.SetActive(true);
        resetButton.SetActive(false);
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

    public void unsetSetCard()
    {
        foreach (var card in cards)
        {
            if (card.GetComponent<FindExtraCard>().IsChosen == true)
            {
                card.GetComponent<FindExtraCard>().IsChosen = false;

                setCardHolderSprite(card, CardSetSuccessFail.Holder);
            }
        }
    }

    public void playSound(Sounds _sound)
    {
        switch (_sound)
        {
            case Sounds.Success:
                audioSource.clip = successAudio;
                break;
            case Sounds.Fail:
                audioSource.clip = failAudioClip;
                break;
            case Sounds.Set:
                audioSource.clip = setCardAudio;
                break;
        }
        
        audioSource.Play();
    }
}
