using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float speed = 2f;

    public float minMoveTime = 2f;
    public float maxMoveTime = 5f;

    public float minIdleTime = 1f;
    public float maxIdleTime = 3f;

    int currentPoint;
    float stateTimer;
    bool isMoving;

    void Start()
    {
        PickRandomPoint();
        StartMoving();
    }

    void Update()
    {
        stateTimer -= Time.deltaTime;

        if (isMoving)
        {
            MoveToPoint();

            if (stateTimer <= 0)
            {
                StartIdle();
            }
        }
        else
        {
            if (stateTimer <= 0)
            {
                PickRandomPoint();
                StartMoving();
            }
        }
    }

    void MoveToPoint()
    {
        Transform target = patrolPoints[currentPoint];

        transform.position = Vector2.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        Flip(target);
    }

    void StartMoving()
    {
        isMoving = true;
        stateTimer = Random.Range(minMoveTime, maxMoveTime);
    }

    void StartIdle()
    {
        isMoving = false;
        stateTimer = Random.Range(minIdleTime, maxIdleTime);
    }

    void PickRandomPoint()
    {
        int newPoint;

        do
        {
            newPoint = Random.Range(0, patrolPoints.Length);
        }
        while (newPoint == currentPoint);

        currentPoint = newPoint;
    }

    void Flip(Transform target)
    {
        Vector3 scale = transform.localScale;

        if (target.position.x > transform.position.x)
            scale.x = Mathf.Abs(scale.x);
        else
            scale.x = -Mathf.Abs(scale.x);

        transform.localScale = scale;
    }
}