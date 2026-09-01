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
    [SerializeField] TextMeshProUGUI tmp;
    [SerializeField] GameObject bubble;
    [SerializeField] TextMeshPro bubbleTxt;
    string gameName;
    string parsed;

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
        tmp = GameObject.Find("MissionText").GetComponent<TextMeshProUGUI>();
        gameName = Application.productName;
        parsed = gameName.ToLower();
        parsed = parsed.Replace("'", "");
        tmp.text = $"<size=50><color=yellow>Mission 3 - It's in the name</color></size>\r\n\r\nwelcome to <size=50><color=yellow>{gameName}</color></size> learning project!\r\nWait... Deja vu... Kinda...\r\n\r\nMission\r\n<color=yellow>Fix the project name</color>\r\n\r\n\r\n";


    }


    void Update()
    {
        if(parsed == "codys unity adventures")
        {
            Trophy.instance.Toggle(true);
            bubble.SetActive(false);
        }
        else
        {
            Trophy.instance.Toggle(false);
            bubble.SetActive(true);
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
