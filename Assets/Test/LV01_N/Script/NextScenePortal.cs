using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScenePortal : MonoBehaviour
{
    public string nextSceneName;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}