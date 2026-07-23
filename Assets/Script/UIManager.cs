using UnityEngine;
using UnityEngine.UI;
 
// Wires GameManager events to on-screen Text elements.
// Assign each field in the Inspector to a UI Text object in your Canvas.
public class UIManager : MonoBehaviour
{
    [Header("HUD")]
    public Text distanceText;
    public Text timerText;
    public Text diamondsText;
 
    [Header("Game Over Panel")]
    public GameObject gameOverPanel;
    public Text finalScoreText;
    public Text highScoreText;
 
    public void UpdateDistance(float distance)
    {
        if (distanceText != null) distanceText.text = $"Distance: {distance:F0} m";
    }
 
    public void UpdateTimer(float time)
    {
        if (timerText != null) timerText.text = $"Next CP: {Mathf.Max(0f, time):F1}s";
    }
 
    public void UpdateDiamonds(int count)
    {
        if (diamondsText != null) diamondsText.text = $"Diamonds: {count}";
    }
 
    public void ShowGameOver(float distance, float highScore)
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        if (finalScoreText != null) finalScoreText.text = $"Distance: {distance:F0} m";
        if (highScoreText != null) highScoreText.text = $"Best: {highScore:F0} m";
    }
 
    // Hook this up to the Restart button's OnClick
    public void OnRestartButton()
    {
        GameManager.Instance.RestartGame();
    }
}
