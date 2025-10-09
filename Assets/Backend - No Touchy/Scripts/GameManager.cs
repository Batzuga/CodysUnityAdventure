using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;

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
    int startDiamonds;
    int currentDiamonds;

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
        startP = player.transform.position;
        Diamond[] dims = FindObjectsByType<Diamond>(FindObjectsSortMode.None);
        startDiamonds = dims.Length;
    }
    public void HideBubble()
    {
        bubble.SetActive(false);
    }
    void Update()
    {
        if (Vector2.Distance(player.transform.position, startP) > 0.2f)
        {
            bubble.SetActive(false);
        }     
        if(startDiamonds != 4)
        {
            Trophy.instance.Toggle(false);
            Debug.LogWarning("There should be 4 diamonds! Shame on you for deleting them!");
            return;
        }
        else if(startDiamonds == 4 && currentDiamonds == 0)
        {
            try
            {
                if (UIManager.instance.scoreText.text == startDiamonds.ToString())
                {
                    Trophy.instance.Toggle(true);
                }
            }
            catch
            {
                Trophy.instance.Toggle(false);
            }
            
        }
        else Trophy.instance.Toggle(false);
    }

    public bool MissionComplete()
    {
        if (!Trophy.instance.EndGame()) return false;
        audioSource.Play();
        winScreen.SetActive(true);
        return true;
    }
}
