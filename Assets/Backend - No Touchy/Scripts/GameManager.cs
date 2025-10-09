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
    int toggleCount;
    int switchCount;
    bool previous;
    [SerializeField] GameObject missionPopup;

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
        previous = missionPopup.activeSelf;
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
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            toggleCount++;
        }
        if(missionPopup.activeSelf != previous)
        {
            previous = !previous;
            switchCount++;
        }
        if(missionPopup == null)
        {
            Trophy.instance.Toggle(false);
        }
        else if(switchCount >= 6 && toggleCount >= 6 && toggleCount == switchCount)
        {
            Trophy.instance.Toggle(true);
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
