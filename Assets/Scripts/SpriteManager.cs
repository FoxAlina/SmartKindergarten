using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    [SerializeField]
    List<Sprite> goodCards;
    [SerializeField]
    List<Sprite> badCards;
    [SerializeField]
    List<Sprite> goodSilhouettes;
    [SerializeField]
    List<Sprite> badSilhouettes;
    [SerializeField]
    int silhouettesScaleFactor;
    [SerializeField]
    Sprite cardHolder;
    [SerializeField]
    Sprite cardSet;
    [SerializeField]
    Sprite cardSuccess;
    [SerializeField]
    Sprite cardFail;
    [SerializeField]
    Sprite silhouetteSuccess;
    [SerializeField]
    Sprite silhouetteFail;

    public Sprite CardHolder
    {
        get { return cardHolder; }
    }

    public int SilhouettesScaleFactor
    {
        get { return silhouettesScaleFactor; }
    }

    public Sprite CardSet
    {
        get { return cardSet; }
    }

    public Sprite CardSuccess
    {
        get { return cardSuccess; }
    }

    public Sprite CardFail
    {
        get { return cardFail; }
    }

    public Sprite SilhouetteSuccess
    {
        get { return silhouetteSuccess; }
    }

    public Sprite SilhouetteFail
    {
        get { return silhouetteFail; }
    }

    public Sprite getRandomCardSprite(bool _isGood)
    {
        Sprite ret;

        goodCards = goodCards.OrderBy(x => Random.value).ToList();
        badCards = badCards.OrderBy(x => Random.value).ToList();

        if (_isGood)
        {
            ret = goodCards[Random.Range(0, goodCards.Count)];
        }
        else
        {
            ret = badCards[Random.Range(0, badCards.Count)];
        }

        return ret;
    }

    public Sprite getRandomSilhouetteSprite()
    {
        Sprite ret;

        List<Sprite> silhouettes = new List<Sprite>();

        silhouettes.AddRange(goodSilhouettes);
        silhouettes.AddRange(badSilhouettes);

        silhouettes = silhouettes.OrderBy(x => Random.value).ToList();

        ret = silhouettes[Random.Range(0, silhouettes.Count)];

        return ret;
    }
}
