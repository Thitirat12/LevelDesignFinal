using UnityEngine;

public class CameraFollowerT : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 0, -10);

    [Header("Smoothing")]
    public float smoothTime = 0.2f;
    private Vector3 velocity = Vector3.zero;

    [Header("Deadzone")]
    public float deadzoneX = 1.5f;
    public float deadzoneY = 0.8f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetPos = target.position + offset;
        Vector3 delta = targetPos - transform.position;

        // เพิ่ม Hysteresis — มี 2 threshold แยกกัน
        if (Mathf.Abs(delta.x) < deadzoneX) targetPos.x = transform.position.x;
        if (Mathf.Abs(delta.y) < deadzoneY) targetPos.y = transform.position.y;
        targetPos.z = offset.z; // ← แก้จาก transform.position.z เป็น offset.z

        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPos,
            ref velocity,
            smoothTime
        );
    }
}