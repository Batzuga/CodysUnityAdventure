using UnityEngine;

public class Lock : MonoBehaviour
{
    [SerializeField] int lockID;
    Animator animator;
    public bool opened { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Key>())
        {
            if(collision.gameObject.GetComponent<Key>().keyID == lockID)
            {
                Open();
                Destroy(collision.gameObject);
            }
        }
    }

    void Open()
    {
        opened = true;
        animator.SetBool("Open", true);
    }
}
