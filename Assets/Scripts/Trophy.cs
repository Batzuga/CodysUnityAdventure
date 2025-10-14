using UnityEngine;

public class Trophy : MonoBehaviour
{
    public static Trophy instance;
    Animator anim;
    [HideInInspector] public bool opened;
    Player player;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (instance != null)
        {
            if (instance != this)
            {
                Destroy(this);
            }
        }
        opened = false;
        anim = GetComponent<Animator>();
    }


    private void Update()
    {
        if(player == null) player = GameObject.FindFirstObjectByType<Player>();
        if(player != null)
        {
            if(player.transform.position.x > 0)
            {
                Debug.Log("Nyahaha");
                transform.position = new Vector2(player.transform.position.x + 6f, -1);
            }
        }

    }

    public void Toggle(bool enabled)
    {
        if(enabled && !opened)
        {
            anim.SetBool("GoalActive", true);
        }
        if(!enabled && opened) 
        {
            anim.SetBool("GoalActive", false);
        }

        opened = enabled;
    }
    public bool EndGame()
    {
        return opened;
    }
}
