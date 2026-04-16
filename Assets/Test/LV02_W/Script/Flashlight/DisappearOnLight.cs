using UnityEngine;

public class DisappearOnLight : MonoBehaviour, ILightReactive
{
    public float disappearTime = 2f;

    private float timer;
    private bool isLit;

    private SpriteRenderer sr;
    private Collider2D col;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
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
            sr.enabled = false;
            col.enabled = false;
        }
        else
        {
            sr.enabled = true;
            col.enabled = true;
        }
    }

    public void OnLightStay()
    {
        isLit = true;
        Debug.Log("Light Detect Object");
    }

}
