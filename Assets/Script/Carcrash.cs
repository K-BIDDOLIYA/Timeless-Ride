using UnityEngine;

// Attach to the car body (same object as CarController).
// Ends the run if the car flips too far past vertical and stays
// that way, or slams into the ground too hard — mimics Hill Climb
// Racing's "crash on landing on your head / hard impact" behavior.
[RequireComponent(typeof(Rigidbody2D))]
public class CarCrash : MonoBehaviour
{
    [Header("Flip Detection")]
    public float flipAngleThreshold = 80f;   // degrees from upright considered "flipped"
    public float flipGraceTime = 1.0f;       // seconds allowed past that angle before crash

    [Header("Impact Detection (optional)")]
    public bool useImpactCrash = true;
    public float minImpactSpeedToCrash = 12f; // relative collision speed that counts as a crash
    public string groundTag = "Ground";       // tag your ground chunk prefab with this

    private float flipTimer = 0f;

    void Update()
    {
        if (GameManager.Instance == null || GameManager.Instance.isGameOver) return;

        float angle = Mathf.Abs(Mathf.DeltaAngle(0f, transform.eulerAngles.z));

        if (angle > flipAngleThreshold)
        {
            flipTimer += Time.deltaTime;
            if (flipTimer >= flipGraceTime) Crash();
        }
        else
        {
            flipTimer = 0f;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!useImpactCrash) return;
        if (GameManager.Instance == null || GameManager.Instance.isGameOver) return;
        if (!collision.collider.CompareTag(groundTag)) return;

        if (collision.relativeVelocity.magnitude >= minImpactSpeedToCrash)
        {
            Crash();
        }
    }

    void Crash()
    {
        if (AudioManager.Instance != null) AudioManager.Instance.PlayCrash();
        GameManager.Instance.GameOver();
    }
}
