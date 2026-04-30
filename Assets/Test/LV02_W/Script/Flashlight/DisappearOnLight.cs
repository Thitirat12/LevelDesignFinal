using UnityEngine;
using UnityEngine.Tilemaps;

public class DisappearOnLight : MonoBehaviour, ILightReactive
{
    public float disappearTime = 2f;

    private float timer;
    private bool isLit;

    private TilemapRenderer tr;
    private Collider2D col;

    void Start()
    {
        tr = GetComponent<TilemapRenderer>();
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (isLit)
        {
            timer = disappearTime;
            isLit = false;
        }
        else
        {
            timer -= Time.deltaTime;
        }

        if (timer > 0)
        {
            tr.enabled = false;
            col.enabled = false;
        }
        else
        {
            tr.enabled = true;
            col.enabled = true;
        }
    }

    public void OnLightStay()
    {
        isLit = true;
        Debug.Log("Light Detect Object");
    }

}
