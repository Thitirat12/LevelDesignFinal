using UnityEngine;

public class SpiderPressureSpawner : MonoBehaviour
{
    [Header("References")]
    public LockPickSystem lockpickSystem;
    public SpiderDropper spiderDropper;

    [Header("Timing")]
    public float minDelay = 4f;
    public float maxDelay = 8f;
    [Range(0f, 1f)]
    public float spawnChance = 0.7f;

    [Header("Limits")]
    public bool limitSpawns = false;
    public int maxSpawns = 3;

    float timer = 0f;
    int spawnCount = 0;

    void Start()
    {
        ResetTimer();
    }

    void Update()
    {
        if (lockpickSystem == null || spiderDropper == null) return;
        if (!lockpickSystem.IsEncounterActive) return;
        if (limitSpawns && spawnCount >= maxSpawns) return;
        if (spiderDropper.IsBusy) return;

        timer -= Time.deltaTime;

        if (timer > 0f) return;

        TrySpawnSpider();
        ResetTimer();
    }

    public void ResetSpawner()
    {
        spawnCount = 0;
        ResetTimer();
    }

    void TrySpawnSpider()
    {
        if (Random.value > spawnChance) return;

        spawnCount++;
        spiderDropper.StartDrop();
    }

    void ResetTimer()
    {
        timer = Random.Range(minDelay, maxDelay);
    }
}
