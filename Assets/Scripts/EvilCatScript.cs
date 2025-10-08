using UnityEngine;

public class EvilCatScript : MonoBehaviour
{
    void Start()
    {
        Destroy(GameObject.FindFirstObjectByType<Trophy>().gameObject);
        GameObject.Find("Francis").transform.GetChild(0).gameObject.SetActive(true);
        Debug.Log("Nya-ha-haa!");
    }

}
