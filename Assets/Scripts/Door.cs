using UnityEngine;

public class Door : MonoBehaviour
{
    BoxCollider2D col;
    SpriteRenderer rend;
    public Sprite doorOpenImage;
    public bool doorOpened { get; private set; }

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
    }
    
    public void Open()
    {
        doorOpened = true;
        //change renderer image to new dooropenimage
        //disable boxcollider
    }
}
