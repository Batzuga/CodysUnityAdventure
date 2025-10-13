using UnityEngine;

public class OverlapCheck : MonoBehaviour
{
    SpriteRenderer rend;
    bool isOverlapping;
    public static OverlapCheck instance;

    [Tooltip("The tag the overlap box checks to see if there is overlap.")]
    public string targetTag;

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
                Destroy(gameObject);
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    public bool Check()
    {
        return isOverlapping;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("GoalTarget"))
        {
            rend.color = new Color(0, 1, 0, 0.33f);
            isOverlapping = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("GoalTarget"))
        {
            rend.color = new Color(1, 0, 0, 0.33f);
            isOverlapping = false;
        }
    }
}
