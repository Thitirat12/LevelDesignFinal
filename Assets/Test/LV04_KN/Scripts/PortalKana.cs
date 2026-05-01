using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalKana : MonoBehaviour
{
    public string sceneName;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Next level");
            SceneManager.LoadScene(sceneName);
        }

        else
        {
            return;
        }
    }
}