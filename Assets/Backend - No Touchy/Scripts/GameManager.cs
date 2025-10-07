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
    int c;
    public List<GameObject> diamonds;
    List<GameObject> collected;

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
        
    }

    public void CollectDiamond(Diamond d)
    {
        c++;
        if(collected == null) collected = new List<GameObject>();
        collected.Add(d.gameObject);
    }
    bool CompareLists()
    {
        if (collected.Count == diamonds.Count)
        {
            bool b = true;
            for(int i = 0; i < collected.Count; i++)
            {
                if (collected[i] != diamonds[i]) b = false;
            }
            return b;
        }
        else return false;
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
        if(c == 0 || !CompareLists())
        {
            Trophy.instance.Toggle(false);
        }
        else
        {
            Trophy.instance.Toggle(true);
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
