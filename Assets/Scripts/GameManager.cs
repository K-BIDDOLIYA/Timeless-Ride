using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public TextMeshProUGUI timerText;
    public GameObject deathPanel;

    [Header("Player")]
    public GameObject car;
    public GameObject explosionPrefab;

    [Header("Timer")]
    public float timeRemaining = 20f;

    public bool counting = false;
    bool dead = false;
    public TextMeshProUGUI scoreText;

    private int score = 0;
    public TextMeshProUGUI finalScoreText;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        deathPanel.SetActive(false);

        timeRemaining = 20f;
        counting = true;
        score = 0;
    scoreText.text = "0";
    }

    void Update()
    {
        if (!counting)
            return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            Die();
        }

        timerText.text = Mathf.CeilToInt(timeRemaining).ToString();
    }

    public void AddTime()
    {
        timeRemaining += 20f;

        score++;

        scoreText.text = score.ToString();

        AudioManager.Instance.PlayClock();
    }

    void Die()
    {
        dead = true;
        counting = false;

        if (explosionPrefab != null)
            Instantiate(explosionPrefab, car.transform.position, Quaternion.identity);

        car.SetActive(false);

        finalScoreText.text = "Final Score: " + score;
        AudioManager.Instance.PlayDeath();
        deathPanel.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}

