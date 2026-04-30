using UnityEngine;
using UnityEngine.Tilemaps;

public class AppearOnLight : MonoBehaviour, ILightReactive
{
    public float appearTime = 2f;

    private float timer;
    private bool isLit;

    private SpriteRenderer sr;
    private Collider2D col;
    private TilemapRenderer tr;

    void Start()
    {
        tr = GetComponent<TilemapRenderer>();
        col = GetComponent<Collider2D>();
        tr.enabled = false;
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
            tr.enabled = true;
            col.isTrigger = false;
        }
        else
        {
            tr.enabled = false;
            col.isTrigger = true;
        }
    }

    public void OnLightStay()
    {
        isLit = true;
        Debug.Log("Light Detect Object");
    }
}
