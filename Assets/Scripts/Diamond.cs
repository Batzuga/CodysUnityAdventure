using UnityEngine;

public class Diamond : MonoBehaviour
{
    public void Collect()
    {
        Debug.Log("Diamond collected!");
        Destroy(gameObject);
    }
}
