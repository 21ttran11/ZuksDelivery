using UnityEngine;

public class LockRotation : MonoBehaviour
{
    private Quaternion lockedRotation;

    void Start()
    {
        // Set the locked rotation to the initial rotation of the canvas in world space
        lockedRotation = Quaternion.Euler(0, 180, 0);
    }

    void LateUpdate()
    {
        // Override any rotation changes that happened during Update
        transform.rotation = lockedRotation;
    }
}
