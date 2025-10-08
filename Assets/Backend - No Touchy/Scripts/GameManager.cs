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
    [SerializeField] GameObject oliverBubble;

    bool fixd;
    float dur;

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
        if(player.GetComponent<Rigidbody2D>().linearVelocityX > 0 && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        {
            dur += Time.deltaTime;
            if (dur > 0.3f)
            {
                fixd = true;
            }
        }
        else
        {
            dur = 0;
        }
        oliverBubble.SetActive(!fixd);
        Trophy.instance.Toggle(fixd);
    }

    public bool MissionComplete()
    {
        if (!Trophy.instance.EndGame()) return false;
        audioSource.Play();
        winScreen.SetActive(true);
        return true;
    }
}
