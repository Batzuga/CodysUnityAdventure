using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Streetlight : MonoBehaviour
{
    [SerializeField] Light2D lightSource;
    [SerializeField] Sprite lampOn;
    [SerializeField] Sprite lampOff;
    public SpriteRenderer rend;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    public void SetLightOn(bool lightOn)
    {
        ChangeTexture(lightOn);
        lightSource.enabled = lightOn;
        lightSource.gameObject.SetActive(lightOn);
    }

    void ChangeTexture(bool b)
    {
        rend.sprite = b ? lampOn : lampOff;
    }


    //helper function for gamemanager
    public bool LampOnCheck()
    {
        return rend.sprite == lampOn && lightSource.isActiveAndEnabled && lightSource.intensity == 1;
    }
}
