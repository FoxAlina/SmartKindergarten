using TMPro;
using UnityEngine;

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

    protected SpriteManager spriteManager;
    protected DataManager dataManager;

    public bool GameOver { get; set; }

    public void initRounds()
    {
        roundsCounterTMP.text = "Раунд: " + dataManager.Counter + " / " + dataManager.Rounds;
    }

    public void countRounds()
    {
        dataManager.countRounds();

        roundsCounterTMP.text = "Раунд: " + dataManager.Counter + " / " + dataManager.Rounds;

        if (dataManager.checkRounds())
        {
            gameOverPanel.SetActive(true);

            GameOver = true;
        }
    }
}
