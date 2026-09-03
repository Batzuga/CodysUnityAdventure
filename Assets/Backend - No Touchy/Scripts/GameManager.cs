using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Windows;

/// <summary>
/// Don't make changes to the Game Manager. It keeps track that you're not cheating ;).
/// </summary>
/// 
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject winScreen;
    Player player;
    [SerializeField] SpriteRenderer tmp;
    [SerializeField] GameObject bubble;
    [SerializeField] TextMeshPro bubbleTxt;
    Sprite sprite;
    Vector2 playerStartp;
    bool textureFixd;
    bool ppu;
    bool filt;
    bool size;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        if(instance != null)
        {
            if(instance != this)
            {
                Destroy(this);
            }
        }
        DontDestroyOnLoad(gameObject);
        player = GameObject.FindFirstObjectByType<Player>();
        playerStartp = player.transform.position;
        sprite = tmp.GetComponent<SpriteRenderer>().sprite;
    }


    void Update()
    {
        ppu = (sprite.pixelsPerUnit == 16);
        filt = sprite.texture.filterMode == FilterMode.Point;
        if(tmp.transform.localScale != Vector3.one) tmp.transform.localScale = Vector3.one;
        size = tmp.transform.localScale.x == 1 && tmp.transform.localScale.y == 1;
        if(filt && ppu && size && !textureFixd)
        {
            textureFixd = true;
            bubble.SetActive(false);
            Trophy.instance.Toggle(true);
        }
        else if (!filt || !ppu || !size)
        {
            textureFixd = false;
            bubble.SetActive(true);
            Trophy.instance.Toggle(false);
        }
    }

    public bool MissionComplete()
    {
        if (!Trophy.instance.EndGame()) return false;
        audioSource.Play();
        winScreen.SetActive(true);
        return true;
    }
}
