using UnityEngine;

// Attach to the checkpoint prefab (BoxCollider2D, Is Trigger = ON).
// When the car (tagged "Player") passes through, reset the countdown.
public class Checkpoint : MonoBehaviour
{
    private bool triggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;
        if (!other.CompareTag("Player")) return;

        triggered = true;
        GameManager.Instance.PassCheckpoint();
        if (AudioManager.Instance != null) AudioManager.Instance.PlayCheckpoint();
        Destroy(gameObject, 0.2f);
    }
}
