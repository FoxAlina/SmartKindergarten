using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    [SerializeField]
    Sprite defaultSprite;
    [SerializeField]
    List<Sprite> goodCards;
    [SerializeField]
    List<Sprite> badCards;
    [SerializeField]
    List<Sprite> goodSilhouettes;
    [SerializeField]
    List<Sprite> badSilhouettes;
    [SerializeField]
    int silhouettesScaleFactor = 1;
    [SerializeField]
    Sprite cardHolder;
    [SerializeField]
    Sprite cardSet;
    [SerializeField]
    Sprite cardSuccess;
    [SerializeField]
    Sprite cardFail;
    [SerializeField]
    List<Sprite> puzzles;
    [SerializeField]
    List<Sprite> backgroundSprites;
    [SerializeField]
    List<Sprite> glSuns;
    [SerializeField]
    List<Sprite> glHighs;
    [SerializeField]
    List<Sprite> glMiddles;
    [SerializeField]
    List<Sprite> glLows;
    [SerializeField]
    Sprite happyCharacter;
    [SerializeField]
    Sprite sadCharacter;
    [SerializeField]
    Sprite idleCharacter;
    [SerializeField]
    List<Sprite> congratsCharacters;
    [SerializeField]
    protected float congratsScaleRate;

    public Sprite CardHolder
    {
        get { return cardHolder; }
    }

    public int SilhouettesScaleFactor
    {
        get { return silhouettesScaleFactor; }
    }

    public float CongratsScaleRate
    {
        get { return congratsScaleRate; }
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

    public Sprite getHappyIdleSad(HappySadIdle _happySadIdle)
    {
        Sprite ret = defaultSprite;

        switch (_happySadIdle)
        {
            case HappySadIdle.Happy:
                ret = happyCharacter;
                break;
            case HappySadIdle.Idle:
                ret = idleCharacter;
                break;
            case HappySadIdle.Sad:
                ret = sadCharacter;
                break;
        }

        return ret;
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

    public Sprite getRandomGLBackgroundSprite()
    {
        Sprite ret;

        backgroundSprites = backgroundSprites.OrderBy(x => Random.value).ToList();

        ret = backgroundSprites[Random.Range(0, backgroundSprites.Count)];

        return ret;
    }

    public Sprite getRandomGLSilhouetteSprite(GoodLandscapeSilhouettes _goodLandscapeSilhouettes)
    {
        Sprite ret = defaultSprite;

        switch (_goodLandscapeSilhouettes)
        {
            case GoodLandscapeSilhouettes.Sun:
                glSuns = glSuns.OrderBy(x => Random.value).ToList();
                ret = glSuns[Random.Range(0, glSuns.Count)];
                break;
            case GoodLandscapeSilhouettes.High:
                glHighs = glHighs.OrderBy(x => Random.value).ToList();
                ret = glHighs[Random.Range(0, glHighs.Count)];
                break;
            case GoodLandscapeSilhouettes.Middle:
                glMiddles = glMiddles.OrderBy(x => Random.value).ToList();
                ret = glMiddles[Random.Range(0, glMiddles.Count)];
                break;
            case GoodLandscapeSilhouettes.Low:
                glLows = glLows.OrderBy(x => Random.value).ToList();
                ret = glLows[Random.Range(0, glLows.Count)];
                break;
        }

        return ret;
    }

    public Sprite getRandomBackgroundSprite()
    {
        Sprite ret;

        backgroundSprites = backgroundSprites.OrderBy(x => Random.value).ToList();

        ret = backgroundSprites[Random.Range(0, backgroundSprites.Count)];

        return ret;
    }

    public Sprite getRandomPuzzle()
    {
        Sprite ret;

        puzzles = puzzles.OrderBy(x => Random.value).ToList();

        ret = puzzles[Random.Range(0, puzzles.Count)];

        return ret;
    }

    public Sprite getRandomCongratsCharacter()
    {
        Sprite ret;

        congratsCharacters = congratsCharacters.OrderBy(x => Random.value).ToList();

        ret = congratsCharacters[Random.Range(0, congratsCharacters.Count)];

        return ret;
    }
}
