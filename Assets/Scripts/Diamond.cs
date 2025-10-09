using UnityEngine;

public class Diamond : MonoBehaviour
{
    public void Collect()
    {
        UIManager.instance.AddScore();
        Destroy(gameObject);
    }
}
