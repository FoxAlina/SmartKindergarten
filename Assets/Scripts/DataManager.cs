using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField]
    int initialRounds;

    public int Rounds { get; set; }
    public int RoundsCounter { get; set; }

    public int EntryLevel { get; set; }

    private void Awake()
    {
        Rounds = PlayerPrefs.GetInt("Rounds");

        if (Rounds == 0)
        {
            Rounds = initialRounds;

            PlayerPrefs.SetInt("Rounds", Rounds);
        }

        EntryLevel = PlayerPrefs.GetInt("EntryLevel");
    }

    public void saveRounds(int _value)
    {
        Rounds = _value;

        PlayerPrefs.SetInt("Rounds", Rounds);
    }

    public void countRounds()
    {
        RoundsCounter++;
    }

    public bool checkRounds()
    {
        return Rounds == RoundsCounter;
    }

    public void countEntries()
    {
        EntryLevel++;

        PlayerPrefs.SetInt("EntryLevel", EntryLevel);
    }
}
