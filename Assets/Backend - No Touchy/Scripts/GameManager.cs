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
    [SerializeField] SpriteRenderer woods;

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
            if(woods.drawMode == SpriteDrawMode.Tiled)
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
