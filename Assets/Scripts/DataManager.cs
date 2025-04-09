using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField]
    int initialRounds;

    public int Rounds { get; set; }
    public int Counter { get; set; }

    private void Awake()
    {
        Rounds = PlayerPrefs.GetInt("Rounds");

        if (Rounds == 0)
        {
            Rounds = initialRounds;

            PlayerPrefs.SetInt("Rounds", Rounds);
        }
    }

    public void saveRounds(int _value)
    {
        Rounds = _value;

        PlayerPrefs.SetInt("Rounds", Rounds);
    }

    public void countRounds()
    {
        Counter++;
    }

    public bool checkRounds()
    {
        return Rounds == Counter;
    }
}
