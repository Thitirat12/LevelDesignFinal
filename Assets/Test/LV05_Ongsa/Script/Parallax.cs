using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform cameraTransform;
    public float parallaxEffect = 0.5f;

    void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.x = cameraTransform.position.x * parallaxEffect;
        transform.position = pos;
    }
}