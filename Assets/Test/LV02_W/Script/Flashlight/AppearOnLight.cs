using UnityEngine;

public class AppearOnLight : MonoBehaviour, ILightReactive
{
    public float appearTime = 2f;

    private float timer;
    private bool isLit;

    private SpriteRenderer sr;
    private Collider2D col;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        sr.enabled = false;
        col.isTrigger = true;
    }

    void Update()
    {
        if (isLit)
        {
            timer = appearTime;
            isLit = false;
        }
        else
        {
            timer -= Time.deltaTime;
        }

        if (timer > 0)
        {
            sr.enabled = true;
            col.isTrigger = false;
        }
        else
        {
            sr.enabled = false;
            col.isTrigger = true;
        }
    }

    public void OnLightStay()
    {
        isLit = true;
        Debug.Log("Light Detect Object");
    }
}
