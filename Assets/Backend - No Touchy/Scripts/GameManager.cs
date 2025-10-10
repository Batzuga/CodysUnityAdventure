using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        Debug.Log(scene.buildIndex);
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
        Trophy.instance.Toggle(reset > 2);
    }

    public bool MissionComplete()
    {
        if (!Trophy.instance.EndGame()) return false;
        audioSource.Play();
        winScreen.SetActive(true);
        return true;
    }
}
