using System;
using TMPro;
using UnityEngine;

public class MainMenuGameManager : MonoBehaviour
{
    [SerializeField]
    TMP_InputField inputField;

    DataManager dataManager;

    private void Start()
    {
        dataManager = GetComponent<DataManager>();

        inputField.text = dataManager.Rounds.ToString();
    }

    public void setRoundNumber(string _value)
    {
        dataManager.saveRounds(Convert.ToInt32(_value));
    }
}
