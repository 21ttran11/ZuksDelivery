using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform cameraTransform;
    private Vector3 offset;

    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
        offset = transform.position - cameraTransform.position;
    }

    void LateUpdate()
    {
        transform.position = cameraTransform.position + offset;
    }
}
