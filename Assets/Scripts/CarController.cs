using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 10f;

    [Header("Limits")]
    public float minY = -4f;
    public float maxY = 4f;

    private float fixedX;

    void Start()
    {
        fixedX = transform.position.x;
    }

    void Update()
    {
        float input = Input.GetAxisRaw("Vertical");

        Vector3 pos = transform.position;

        pos.y += input * moveSpeed * Time.deltaTime;

        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        // Keep the car at the same X position
        pos.x = fixedX;

        transform.position = pos;
    }
}
