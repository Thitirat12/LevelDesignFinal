using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string sceneName;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (GameManagerTop.instance != null && GameManagerTop.instance.hasKey)
        {
            Debug.Log("มีกุญแจ → ไปฉากถัดไป");
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.Log("ยังไม่มีกุญแจ!");
        }
    }
}