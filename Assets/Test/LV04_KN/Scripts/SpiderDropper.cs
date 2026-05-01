using UnityEngine;

public class SpiderDropper : MonoBehaviour
{
    [Header("Drop Points")]
    public Transform ceilingPoint;
    public Transform doorHeadPoint;

    [Header("Mini Game")]
    public SpiderMiniGame spiderMiniGame;
    public bool startMiniGameWhenArrived = true;

    [Header("Movement")]
    public float dropSpeed = 2f;
    public bool hideOnStart = true;
    public GameObject visualRoot;

    bool isDropping = false;
    bool hasArrived = false;
    public bool IsBusy => isDropping || (spiderMiniGame != null && spiderMiniGame.IsActive);

    void Start()
    {
        if (ceilingPoint != null)
        {
            transform.position = ceilingPoint.position;
        }

        if (hideOnStart)
        {
            SetSpiderVisible(false);
        }
    }

    void Update()
    {
        if (!isDropping || hasArrived || doorHeadPoint == null) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            doorHeadPoint.position,
            dropSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, doorHeadPoint.position) <= 0.01f)
        {
            ArriveAtDoorHead();
        }
    }

    public void StartDrop()
    {
        SetSpiderVisible(true);

        if (ceilingPoint != null)
        {
            transform.position = ceilingPoint.position;
        }

        isDropping = true;
        hasArrived = false;
    }

    public void ResetSpider()
    {
        isDropping = false;
        hasArrived = false;

        if (ceilingPoint != null)
        {
            transform.position = ceilingPoint.position;
        }

        if (hideOnStart)
        {
            SetSpiderVisible(false);
        }
    }

    void ArriveAtDoorHead()
    {
        isDropping = false;
        hasArrived = true;

        if (startMiniGameWhenArrived && spiderMiniGame != null)
        {
            spiderMiniGame.StartSpiderMiniGame();
        }
    }

    void SetSpiderVisible(bool isVisible)
    {
        if (visualRoot != null)
        {
            visualRoot.SetActive(isVisible);
            return;
        }

        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>(true);

        foreach (SpriteRenderer spriteRenderer in renderers)
        {
            spriteRenderer.enabled = isVisible;
        }
    }
}
