using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardHolder : MonoBehaviour
{
    public string CardSet { get; set; }

    private void Awake()
    {
        CardSet = string.Empty;
    }

}
