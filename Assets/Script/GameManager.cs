using UnityEngine;
using UnityEngine.SceneManagement;

// Central brain of the game. Lives once in the scene.
// Tracks distance (= score), the countdown to the next checkpoint,
// diamonds collected, and game over / restart / high score.
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Checkpoint Timer")]
    public float checkpointTimeLimit = 10f;   // seconds allowed to reach next checkpoint
    private float timeRemaining;

    [Header("Diamonds")]
    public float diamondTimeBonus = 2f;
    public int diamondsCollected = 0;

    [Header("Score / Distance")]
    public Transform car;                     // assign the car
    private float startX;
    public float distanceTravelled { get; private set; }
    public float highScore { get; private set; }

    public bool isGameOver { get; private set; }

    [Header("UI")]
    public UIManager ui;                      // assign

    void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }
    }

    void Start()
    {
        startX = car.position.x;
        timeRemaining = checkpointTimeLimit;
        highScore = PlayerPrefs.GetFloat("HighScore", 0f);
        isGameOver = false;
    }

    void Update()
    {
        if (isGameOver) return;

        distanceTravelled = Mathf.Max(0f, car.position.x - startX);

        timeRemaining -= Time.deltaTime;
        if (ui != null)
        {
            ui.UpdateTimer(timeRemaining);
            ui.UpdateDistance(distanceTravelled);
        }

        if (timeRemaining <= 0f)
        {
            GameOver();
        }
    }

    // Called by Checkpoint cs
    public void PassCheckpoint()
    {
        timeRemaining = checkpointTimeLimit;
    }

    // called by diamond cs
    public void CollectDiamond()
    {
        diamondsCollected++;
        timeRemaining += diamondTimeBonus;
        if (ui != null) ui.UpdateDiamonds(diamondsCollected);
    }

    public void GameOver()
    {
        isGameOver = true;

        if (distanceTravelled > highScore)
        {
            highScore = distanceTravelled;
            PlayerPrefs.SetFloat("HighScore", highScore);
            PlayerPrefs.Save();
        }

        if (ui != null) ui.ShowGameOver(distanceTravelled, highScore);
        if (AudioManager.Instance != null) AudioManager.Instance.PlayGameOver();
        Time.timeScale = 0f; // freeze the world
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

