using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public float distance = 5f;
    public float angle = 45f;
    public int rayCount = 10;
    public LayerMask targetLayer;

    void Update()
    {
        RotateToMouse();

        if (Input.GetMouseButton(0))
        {
            ShootCone();
        }
    }

    void RotateToMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = mousePos - transform.position;

        float ang = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, ang);
    }

    void ShootCone()
    {
        float startAngle = -angle;
        float step = (angle * 2) / rayCount;

        for (int i = 0; i <= rayCount; i++)
        {
            float currentAngle = startAngle + step * i;

            Vector2 dir = Quaternion.Euler(0, 0, currentAngle) * transform.right;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, distance, targetLayer);

            Debug.DrawRay(transform.position, dir * distance, Color.yellow);

            if (hit.collider != null)
            {
                Debug.Log("Hit: " + hit.collider.name);

                if (hit.collider.CompareTag("Appear"))
                {
                    ILightReactive reactive = hit.collider.GetComponent<AppearOnLight>();

                    if (reactive != null)
                    {
                        reactive.OnLightStay();
                    }
                }

                else if (hit.collider.CompareTag("Disappear"))
                {
                    ILightReactive reactive = hit.collider.GetComponent<DisappearOnLight>();

                    if (reactive != null)
                    {
                        reactive.OnLightStay();
                    }
                }
            }
        }
    }
} 