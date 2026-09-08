using UnityEngine;
public class ShieldFloat : MonoBehaviour
{
    public float floatHeight = 0.25f;
    public float floatSpeed = 2f;
    public float moveAmount = 0.2f;
    public float moveSpeed = 1.5f;
    public float rotationSpeed = 30f;
    private Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;
    }
    void Update()
    {
        float y = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        float x = Mathf.Sin(Time.time * moveSpeed) * moveAmount;
        transform.position = new Vector3(
            startPosition.x + x,
            startPosition.y + y,
            startPosition.z
        );
        transform.Rotate(
            0f,
            0f,
            rotationSpeed * Time.deltaTime
        );
    }
}