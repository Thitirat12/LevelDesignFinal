using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerN : MonoBehaviour
{
    public static GameManagerN instance;

    public int currentItem = 0;
    public int totalItem = 5; // จำนวนของทั้งหมดในด่าน

    void Awake()
    {
        instance = this;
    }

    public void CollectItem()
    {
        currentItem++;
        Debug.Log("Item: " + currentItem);

        if (currentItem >= totalItem)
        {
            Debug.Log("เก็บครบแล้ว!");
        }
    }

    public bool IsComplete()
    {
        return currentItem >= totalItem;
    }

    public void GoNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}