using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Panels")]
    public GameObject settingsPanel;

    private bool paused = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        settingsPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        Debug.Log("PauseGame called");
        paused = true;
        settingsPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        paused = false;
        settingsPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        GameManager.Instance.RestartGame();
    }
}
