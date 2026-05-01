using UnityEngine;

public class AdvancedBlockSpawner : MonoBehaviour
{
    public GameObject blockPrefab;

    public Transform spawnPoint; // จุดยิงเดียว
    public float spawnInterval = 1.5f; // ยิงทุกกี่วิ

    public float blockSpeed = 10f; // ความเร็วคงที่

    void Start()
    {
        InvokeRepeating("SpawnBlock", 1f, spawnInterval);
    }

    void SpawnBlock()
    {
        GameObject block = Instantiate(blockPrefab, spawnPoint.position, Quaternion.identity);

        // ตั้งค่าความเร็วให้ block
        PushBlock pb = block.GetComponent<PushBlock>();
        if (pb != null)
        {
            pb.speed = blockSpeed;
        }
    }
}