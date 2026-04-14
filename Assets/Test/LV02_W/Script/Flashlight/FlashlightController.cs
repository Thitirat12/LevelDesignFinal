using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    [Header("Light Settings")]
    public float lightRadius;
    public float lightAngle;
    public LayerMask targetLayer;

    [Header("References")]
    public Transform lightOrigin;

    private void Update()
    {
        RotateToMouse();
        DetectObjects();
    }

    void RotateToMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - lightOrigin.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void DetectObjects()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            lightOrigin.position,
            lightRadius,
            targetLayer
        );

        foreach (var hit in hits)
        {
            Vector2 directionToTarget = (hit.transform.position - transform.position).normalized;

            float angle = Vector2.Angle(transform.right, directionToTarget);

            if (angle < lightAngle)
            {
                ILightReactive reactive = hit.GetComponent<ILightReactive>();
                if (reactive != null)
                {
                    reactive.OnLightStay();
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Vector3 origin = transform.position;

        Gizmos.DrawWireSphere(origin, lightRadius);

        Vector3 leftDir = Quaternion.Euler(0, 0, -lightAngle) * transform.right;
        Vector3 rightDir = Quaternion.Euler(0, 0, lightAngle) * transform.right;

        Gizmos.DrawRay(origin, leftDir * lightRadius);
        Gizmos.DrawRay(origin, rightDir * lightRadius);
    }
}