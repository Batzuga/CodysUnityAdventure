using UnityEngine;

public class Diamond : MonoBehaviour
{
    public void Collect()
    {
        GameManager.instance.CollectDiamond(this);
        Destroy(gameObject);
    }
}
