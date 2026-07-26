using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    public Transform bg1;
    public Transform bg2;

    public float scrollSpeed = 5f;
    public float backgroundWidth = 40f;

    void Update()
    {
        bg1.position += Vector3.left * scrollSpeed * Time.deltaTime;
        bg2.position += Vector3.left * scrollSpeed * Time.deltaTime;

        if (bg1.position.x <= -backgroundWidth)
        {
            bg1.position = new Vector3(
                bg2.position.x + backgroundWidth,
                bg1.position.y,
                bg1.position.z);
        }

        if (bg2.position.x <= -backgroundWidth)
        {
            bg2.position = new Vector3(
                bg1.position.x + backgroundWidth,
                bg2.position.y,
                bg2.position.z);
        }
    }
}
