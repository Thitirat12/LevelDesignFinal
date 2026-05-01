using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject winUI;
    public GameObject gameOverUI;

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;

        AudioListener.pause = true;
    }

    public void RestartGame()
    {
        AudioListener.pause = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Win()
    {
        winUI.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true;
    }
}