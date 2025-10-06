using System.Text.RegularExpressions;
using System;
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
    Vector2 startP;
    [SerializeField] Door door;
    [SerializeField] DoorSensor sensor;

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
        try
        {
            bool b1 = false;
            if (door.doorOpened && (!door.GetComponent<BoxCollider2D>().enabled || door.GetComponent<BoxCollider2D>().isTrigger) && door.GetComponent<SpriteRenderer>().sprite == door.doorOpenImage)
            {
                b1 = true;
            }
            bool b2 = false;
            if (sensor.GetComponent<Collider2D>().isTrigger && sensor.targetDoor == door)
            {
                b2 = true;
            }
            Trophy.instance.Toggle(b2 && b1);
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
