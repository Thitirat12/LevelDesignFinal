using UnityEngine;

public class GameManagerTop : MonoBehaviour
{
    public static GameManagerTop instance;

    [Header("Player Data")]
    public bool hasKey = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}