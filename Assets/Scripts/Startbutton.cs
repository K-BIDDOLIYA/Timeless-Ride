using UnityEngine;

public class StartButton : MonoBehaviour
{
    [Header("UI")]
    public GameObject startButton;
    public GameObject startText;

    [Header("Game Manager")]
    public GameManager gameManager;

    void Start()
    {
        // Pause the game initially
        Time.timeScale = 0f;

        // Show the start UI
        startButton.SetActive(true);

        if (startText != null)
            startText.SetActive(true);
    }

    public void StartGame()
    {
        // Resume the game
        Time.timeScale = 1f;

        // Start the timer
        gameManager.counting = true;

        // Hide the start UI
        startButton.SetActive(false);

        if (startText != null)
            startText.SetActive(false);
    }
}
