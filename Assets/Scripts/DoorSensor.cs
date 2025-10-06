using UnityEngine;

public class DoorSensor : MonoBehaviour
{
    public Door targetDoor;
    [SerializeField] Sprite sensorOn, sensorOff;
    SpriteRenderer rend;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        rend.sprite = sensorOff;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        rend.sprite = sensorOn;
        if(targetDoor != null)
        {
            targetDoor.Open();
        }
        else
        {
            Debug.LogWarning("targetDoor is null.");
        }
    }

}
