using UnityEngine;

public class StartButton : MonoBehaviour
{
    [Header("References")]
    public GameObject startButton;
    public GameObject startText;
    public GameManager gameManager;

    void Start()
    {
        Time.timeScale = 0f;
        gameManager.counting = false;

        if (startButton != null)
        {
            gameObject.SetActive(true);

            UnityEngine.UI.Button btn = startButton.GetComponent<UnityEngine.UI.Button>();
            if (btn != null)
                btn.interactable = true;
        }

        if (startText != null)
            startText.SetActive(true);
    }

    public void StartGame()
    {
        Debug.Log("Start button clicked!");

        if (startButton != null)
        {
            UnityEngine.UI.Button btn = startButton.GetComponent<UnityEngine.UI.Button>();
            if (btn != null)
                btn.interactable = false;

            gameObject.SetActive(false);
        }

        if (startText != null)
            startText.SetActive(false);

        Time.timeScale = 1f;

        gameManager.counting = true;
    }
}
