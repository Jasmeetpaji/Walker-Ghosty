using UnityEngine;
public class BackgroundFollow : MonoBehaviour
{
    public Transform cameraTransform;
    void LateUpdate()
    {
        if (cameraTransform == null)
            return;
        transform.position = new Vector3(
            cameraTransform.position.x,
            cameraTransform.position.y,
            transform.position.z
        );
    }
}