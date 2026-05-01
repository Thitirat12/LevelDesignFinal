using UnityEngine;

public class PlayerDefense : MonoBehaviour
{
    public float range = 3f;
    public LockPickSystem lockpickSystem;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (lockpickSystem != null && lockpickSystem.IsEncounterActive)
            {
                return;
            }

            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range);

            foreach (var hit in hits)
            {
                if (hit.CompareTag("Enemy"))
                {
                    EnemyAI enemy = hit.GetComponent<EnemyAI>();

                    if (enemy != null)
                    {
                        enemy.Stun();
                    }
                }
            }
        }
    }
}
