using UnityEngine;

public class Trophy : MonoBehaviour
{
    public static Trophy instance;
    Animator anim;
    [HideInInspector] public bool opened;
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
