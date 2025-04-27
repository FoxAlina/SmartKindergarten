using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChooseGoodBadGameManager : BasicGameManager
{
    [SerializeField]
    GameObject card;

    [SerializeField]
    GameObject goodButton;
    [SerializeField]
    GameObject badButton;

    void Start()
    {
        spriteManager = GetComponent<SpriteManager>();
        dataManager = GetComponent<DataManager>();

        this.playExplanationAudio();

        initCongratsCharacter();

        initRounds();

        initiateCard();

        resetButton.SetActive(false);
    }

    public void initiateCard()
    {
        if (!GameOver)
        {
            int random = Random.Range(0, 150);
            int remainder = random % 2;
            bool isGood = remainder > 0;

            Sprite sprite = spriteManager.getRandomCardSprite(isGood);

            card.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
            card.GetComponent<ChooseGoodBadCard>().IsGoodCard = isGood;
        }
    }

    public void checkGood()
    {
        if (card.GetComponent<ChooseGoodBadCard>().IsGoodCard == true)
        {
            success();
        }
        else
        {
            fail();
        }
    }

    public void checkBad()
    {
        if (card.GetComponent<ChooseGoodBadCard>().IsGoodCard == false)
        {
            success();
        }
        else
        {
            fail();
        }
    }

    private void success()
    {
        audioSource.clip = successAudio;
        audioSource.Play();

        card.GetComponent<Image>().sprite = spriteManager.CardSuccess;

        countRounds();

        resetButton.SetActive(!GameOver);
        refreshButton.SetActive(false);
        goodButton.SetActive(false);
        badButton.SetActive(false);

        StartCoroutine(setHappySadCharacter(true));
    }

    private void fail()
    {
        audioSource.clip = failAudioClip;
        audioSource.Play();

        card.GetComponent<Image>().sprite = spriteManager.CardFail;

        StartCoroutine(setHappySadCharacter());
    }

    public void restart()
    {
        initiateCard();

        card.GetComponent<Image>().sprite = spriteManager.CardHolder;

        resetButton.SetActive(false);
        refreshButton.SetActive(true);
        goodButton.SetActive(true);
        badButton.SetActive(true);
    }
}
