using UnityEngine;

public class Trophy : MonoBehaviour
{
    public static Trophy instance;
    Animator anim;
    [HideInInspector] public bool opened;
    bool catsAreCool;
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
        catsAreCool = true;
        anim = GetComponent<Animator>();
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
        if (catsAreCool)
        {
            transform.position = new Vector2(transform.position.x, 2f);
            GameObject.Find("Francis").transform.GetChild(0).gameObject.SetActive(true);
            Debug.Log("Nya-ha-haa!");
            return false;
        }
        
        return opened;
    }
}
