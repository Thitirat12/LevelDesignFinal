using UnityEngine;

public class DisappearPlatform : MonoBehaviour
{
    public float delay = 1f;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Invoke("Hide", delay);
        }
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }
}