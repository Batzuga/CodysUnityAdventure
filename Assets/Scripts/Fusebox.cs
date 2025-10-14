using UnityEngine;

public class Fusebox : MonoBehaviour
{
    [SerializeField] Streetlight targetLamp;
    bool fuseBoxOn;
    SpriteRenderer rend;
    [SerializeField] Sprite boxOn, boxOff;


    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rend.sprite = boxOff;
        fuseBoxOn = false;
    }

    public void PullLever()
    {
        fuseBoxOn = !fuseBoxOn;
        rend.sprite = fuseBoxOn ? boxOn : boxOff;
        targetLamp.SetLightOn();
    }

    //gamemanager helper function no touchy.
    public bool BoxOnCheck()
    {
        return fuseBoxOn && rend.sprite == boxOn;
    }
}
