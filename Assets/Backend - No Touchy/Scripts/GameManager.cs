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
    [SerializeField] Lock[] locks;
    [SerializeField] Key[] keys;

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
        KeyTest();
        
    }

    void KeyTest()
    {
        List<GameObject> hits = new List<GameObject>();
        foreach (Key key in keys)
        {
            Vector2 pos = key.transform.position;
            pos.y = 6.5f;
            float dist = Vector2.Distance(key.transform.position, pos);
            if (dist > 0.1f)
            {
                hits.Add(key.gameObject);
            }
        }
        for(int i = 0; i < hits.Count; i++)
        {
            Destroy(hits[i]);
            bubbleTxt.text = "I wanted that trophy, but not like this... Not like this...";
            Debug.LogError($"Key with id: " + hits[i].GetComponent<Key>().keyID + " destroyed due to foul play. Shame on you. Set the key Y position to 6.5f.");
        }
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
        try
        {
            bool fin = true;
            if(locks.Length == 0) fin = false;
            foreach(Lock l in locks)
            {
                if(l.opened == false)
                {
                    fin = false;
                }
            }
            Trophy.instance.Toggle(fin);
        }
        catch(Exception e)
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
