using UnityEngine;

public class AppearOnLight : MonoBehaviour, ILightReactive
{
    public float appearTime = 2f;

    private float timer;
    private bool isLit;

    private SpriteRenderer sr;
    //private Collider2D col;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        //col = GetComponent<Collider2D>();

        sr.enabled = false;
        gameObject.layer = LayerMask.NameToLayer("TargetObject");
        //col.enabled = false;
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
            gameObject.layer = LayerMask.NameToLayer("Solid");
            //col.enabled = true;
        }
        else
        {
            sr.enabled = false;
            gameObject.layer = LayerMask.NameToLayer("TargetObject");
            //col.enabled = false;
        }
    }

    public void OnLightStay()
    {
        isLit = true;
        Debug.Log("Light Detect Object");
    }
}
