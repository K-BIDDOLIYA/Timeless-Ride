using UnityEngine;

public class TimeStar : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float destroyX = -20f;

    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x < destroyX)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        GameManager.Instance.AddTime();

        Destroy(gameObject);
    }
}
