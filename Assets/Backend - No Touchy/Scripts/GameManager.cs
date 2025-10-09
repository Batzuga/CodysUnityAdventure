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
    bool jumpBeenPressed;
    bool playerboxed;
    [SerializeField] BoxCollider2D lap;
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
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            jumpBeenPressed = true;
        }
        List<Collider2D> results = new List<Collider2D>();
        if(!playerboxed && lap.Overlap(results) > 0)
        {
            foreach(Collider2D col in results)
            {
                if(col.gameObject.CompareTag("Player"))
                {
                    playerboxed = true;
                }
            }
        }
        Trophy.instance.Toggle(playerboxed && jumpBeenPressed);
    }

    public bool MissionComplete()
    {
        if (!Trophy.instance.EndGame()) return false;
        audioSource.Play();
        winScreen.SetActive(true);
        return true;
    }
}
