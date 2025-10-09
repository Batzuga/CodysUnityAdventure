using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] GameObject missionPopup;
    [SerializeField] TextMeshProUGUI scoreText;
    int score;

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
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleMissionPopup();   
        }
    }

    public void SetMissionPopup(bool isOn)
    {
        missionPopup.SetActive(isOn);
    }

    public void ToggleMissionPopup()
    {
        missionPopup.SetActive(!missionPopup.activeSelf);
    }

    public void AddScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
