using System;
using TMPro;
using UnityEngine;

public class MainMenuGameManager : MonoBehaviour
{
    [SerializeField]
    TMP_InputField inputField;
    [SerializeField]
    GameObject mainMenuPanel;
    [SerializeField]
    GameObject exceedEntryLevelPanel;

    DataManager dataManager;

    private void Start()
    {
        dataManager = GetComponent<DataManager>();

        inputField.text = dataManager.Rounds.ToString();

        mainMenuPanel.SetActive(GameSceneHistory.PreviousSceneBuildIndex == 0);

        if (GameSceneHistory.PreviousSceneBuildIndex == 0)
        {
            dataManager.countEntries();

            if (dataManager.EntryLevel == 30)
            {
                exceedEntryLevelPanel.SetActive(true);
            }
        }
    }

    public void setRoundNumber(string _value)
    {
        dataManager.saveRounds(Convert.ToInt32(_value));
    }
}
