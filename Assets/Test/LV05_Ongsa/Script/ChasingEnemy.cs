using UnityEngine;

public class ChasingEnemy : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;

    void Update()
    {
        Vector3 target = new Vector3(player.position.x - 1.5f, transform.position.y, 0);
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Game Over");
            FindObjectOfType<GameManager>().GameOver();

        }
    }
}