using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

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
    Vector2 startP;
    int reset;
    int scenenum;
    int start;
    int cur;
    int prev;
    bool cheater;
    [SerializeField] SpriteRenderer rabbit;
    string curRabbit;
    int loops;
    bool broken;

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
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
        player = GameObject.FindFirstObjectByType<Player>();
        startP = player.transform.position;
        scenenum = SceneManager.GetActiveScene().buildIndex;
        SceneManager.sceneLoaded += LoadScene;
        broken = false;
    }

    private void LoadScene(Scene scene, LoadSceneMode mode)
    {
        player = GameObject.FindFirstObjectByType<Player>();
        startP = player.transform.position;
        if(scene.buildIndex == scenenum) reset++;
        winScreen = GameObject.Find("MissionUI").transform.Find("WinScreen").gameObject;
        try
        {
            audioSource = GetComponent<AudioSource>();
        }
        catch
        {

        }
    }

    public void HideBubble()
    {
        bubble.SetActive(false);
    }
    void Update()
    {
        if(player == null) player = GameObject.FindFirstObjectByType<Player>();

        if (Vector2.Distance(player.transform.position, startP) > 0.2f)
        {
            if (bubble == null) bubble = player.transform.Find("SpeechBubble (Cody)").gameObject;
            bubble.SetActive(false);
        }
        try
        {

            if(rabbit.sprite.name == "hop_0" && curRabbit != rabbit.sprite.name)
            {
                if(loops == 0)
                {
                    curRabbit = rabbit.sprite.name;
                }
                if(loops > 0 && curRabbit == "hop_3")
                {
                    curRabbit = rabbit.sprite.name;
                    loops++;
                }
                else if (loops > 0)
                {
                    broken = true;
                    Debug.LogWarning("Not quite. There is an issue with the animation!");
                }
            }       
            else if(rabbit.sprite.name == "hop_1" && curRabbit != rabbit.sprite.name)
            {
                if(curRabbit == "hop_0")
                {
                    curRabbit = rabbit.sprite.name;
                    loops++;
                }
                else
                {
                    broken = true;
                    Debug.LogWarning("Not quite. There is an issue with the animation!");
                }
            }
            else if (rabbit.sprite.name == "hop_2" && curRabbit != rabbit.sprite.name)
            {
                if (curRabbit == "hop_1")
                {
                    curRabbit = rabbit.sprite.name;
                    loops++;
                }
                else
                {
                    broken = true;
                    Debug.LogWarning("Not quite. There is an issue with the animation!");
                }
            }
            else if (rabbit.sprite.name == "hop_3" && curRabbit != rabbit.sprite.name)
            {
                if (curRabbit == "hop_2")
                {
                    curRabbit = rabbit.sprite.name;
                    loops++;
                }
                else
                {
                    broken = true;
                    Debug.LogWarning("Not quite. There is an issue with the animation!");
                }
            }
            if (broken == false && loops >= 12)
            {
                Trophy.instance.Toggle(true);
            }
            else Trophy.instance.Toggle(false);
        }
        catch
        {
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
