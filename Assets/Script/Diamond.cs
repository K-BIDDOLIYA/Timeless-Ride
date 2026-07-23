using UnityEngine;

// Attach to the diamond prefab (CircleCollider2D, Is Trigger = ON).
// Picking it up grants +2s (set on GameManager) to the checkpoint timer.
public class Diamond : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        GameManager.Instance.CollectDiamond();
        if (AudioManager.Instance != null) AudioManager.Instance.PlayDiamond();
        Destroy(gameObject);
    }
}
