using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BasicGameManager : MonoBehaviour
{
    [SerializeField]
    protected AudioSource audioSource;
    [SerializeField]
    protected AudioClip setCardAudio;
    [SerializeField]
    protected AudioClip successAudio;
    [SerializeField]
    protected AudioClip failAudioClip;

    [SerializeField]
    protected GameObject checkButton;
    [SerializeField]
    protected GameObject resetButton;
    [SerializeField]
    protected GameObject refreshButton;

    [SerializeField]
    protected TMP_Text roundsCounterTMP;
    [SerializeField]
    protected GameObject gameOverPanel;
    [SerializeField]
    protected GameObject congratsCharacter;

    [SerializeField]
    protected GameObject character;
    [SerializeField]
    protected float happySeconds = 3f;

    protected SpriteManager spriteManager;
    protected DataManager dataManager;

    public bool GameOver { get; set; }

    public void initRounds()
    {
        roundsCounterTMP.text = "Раунд: " + dataManager.RoundsCounter + " / " + dataManager.Rounds;
    }

    public void initCongratsCharacter()
    {
        RectTransform rectTransformLocal;

        congratsCharacter.GetComponent<Image>().sprite = spriteManager.getRandomCongratsCharacter();
        
        rectTransformLocal = congratsCharacter.GetComponent<RectTransform>();
        rectTransformLocal.sizeDelta = new Vector2(rectTransformLocal.sizeDelta.x / spriteManager.CongratsScaleRate, rectTransformLocal.sizeDelta.y / spriteManager.CongratsScaleRate);
    }

    public void countRounds()
    {
        dataManager.countRounds();

        roundsCounterTMP.text = "Раунд: " + dataManager.RoundsCounter + " / " + dataManager.Rounds;

        if (dataManager.checkRounds())
        {
            gameOverPanel.SetActive(true);

            GameOver = true;
        }
    }

    protected IEnumerator setHappySadCharacter(bool _happy = false)
    {
        character.GetComponent<Image>().sprite = spriteManager.getHappyIdleSad((_happy) ? HappySadIdle.Happy : HappySadIdle.Sad);

        yield return new WaitForSeconds(happySeconds);

        character.GetComponent<Image>().sprite = spriteManager.getHappyIdleSad(HappySadIdle.Idle);
    }
}
