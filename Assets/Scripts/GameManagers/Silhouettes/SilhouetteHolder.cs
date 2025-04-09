using UnityEngine;

public class SilhouetteHolder : MonoBehaviour
{
    private string linkedSilhouetteName;

    public string LinkedSilhouetteName
    {
        get { return linkedSilhouetteName; }
        set { linkedSilhouetteName = value; }
    }

    private string setSilhouetteName;

    public string SetSilhouetteName
    {
        get { return setSilhouetteName; }
        set { setSilhouetteName = value; }
    }

    private void Awake()
    {
        setSilhouetteName = string.Empty;
    }
}
