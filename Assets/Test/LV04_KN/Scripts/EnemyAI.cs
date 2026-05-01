using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public LockPickSystem lockpickSystem;
    public AudioSource audioSource;
    public AudioClip catchSfx;
    public float speed = 2f;
    public float retreatSpeed = 2.5f;
    public float visibleDistance = 8f;
    public float catchDistance = 0.75f;
    public float retreatStopDistance = 0.1f;
    public LayerMask obstacleMask;
    public Transform retreatPoint;

    bool stunned = false;
    bool isRetreating = false;
    Camera mainCamera;
    Vector3 startPosition;

    void Start()
    {
        mainCamera = Camera.main;
        startPosition = transform.position;
    }

    void Update()
    {
        if (player == null) return;

        if (stunned) return;

        bool isBeingObserved = IsBeingObserved();

        if (isBeingObserved)
        {
            isRetreating = true;
        }

        if (isRetreating)
        {
            KeepSafeDistance();
            return;
        }

        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            speed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, player.position) <= catchDistance)
        {
            CatchPlayer();
        }
    }

    public void Stun()
    {
        stunned = true;
        Invoke(nameof(Recover), 1.5f);
    }

    void Recover()
    {
        stunned = false;
    }

    void CatchPlayer()
    {
        if (audioSource != null && catchSfx != null)
        {
            audioSource.PlayOneShot(catchSfx);
        }

        if (lockpickSystem != null)
        {
            lockpickSystem.OnPlayerCaught();
        }

        ResetEnemyPosition();
        stunned = true;
        CancelInvoke(nameof(Recover));
        Invoke(nameof(Recover), 1f);
    }

    public void ResetEnemyPosition()
    {
        transform.position = startPosition;
        isRetreating = false;
    }

    void KeepSafeDistance()
    {
        Vector3 targetPosition = retreatPoint != null ? retreatPoint.position : startPosition;
        float distanceToTarget = Vector2.Distance(transform.position, targetPosition);

        if (distanceToTarget <= retreatStopDistance)
        {
            isRetreating = false;
            return;
        }

        transform.position = Vector2.MoveTowards(
            transform.position,
            targetPosition,
            retreatSpeed * Time.deltaTime
        );
    }

    bool IsBeingObserved()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        Vector2 toEnemy = transform.position - player.position;

        if (toEnemy.magnitude > visibleDistance)
        {
            return false;
        }

        if (!IsInFrontOfPlayer(toEnemy))
        {
            return false;
        }

        if (mainCamera != null)
        {
            Vector3 viewportPoint = mainCamera.WorldToViewportPoint(transform.position);
            bool inCameraView = viewportPoint.z > 0f &&
                                viewportPoint.x > 0f && viewportPoint.x < 1f &&
                                viewportPoint.y > 0f && viewportPoint.y < 1f;

            if (!inCameraView)
            {
                return false;
            }
        }

        RaycastHit2D hit = Physics2D.Linecast(player.position, transform.position, obstacleMask);
        return hit.collider == null;
    }

    bool IsInFrontOfPlayer(Vector2 toEnemy)
    {
        float facingDirection = player.localScale.x >= 0f ? 1f : -1f;
        return Mathf.Sign(toEnemy.x) == facingDirection || Mathf.Abs(toEnemy.x) < 0.2f;
    }
}
