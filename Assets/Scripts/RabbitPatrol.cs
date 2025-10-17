using UnityEngine;

public class RabbitPatrol : MonoBehaviour
{
    [SerializeField] Vector2 point1, point2;
    Vector2 target;
    [SerializeField] float moveSpeed;
    SpriteRenderer rend;

    bool b;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = point1;
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * moveSpeed);
        if(Vector2.Distance(transform.position, target) < 0.2f)
        {
            b = !b;
            rend.flipX = !b;
            target = b ? point1 : point2;
        }
    }
}
