using UnityEngine;
using UnityEngine.UI;

public class SearchSystem : MonoBehaviour
{
    [Header("UI")]
    public Slider progressBar;
    public GameObject progressUI;

    [Header("Search")]
    public float searchTime = 3f;

    [Header("Item Popup")]
    public GameObject itemPopupPrefab;
    public Transform spawnPoint;

    private float currentTime = 0f;
    private bool isSearching = false;
    private bool playerInRange = false;
    private bool isCompleted = false;

    void Start()
    {
        progressUI.SetActive(false);
        progressBar.value = 0f;
    }

    void Update()
    {
        if (!playerInRange || isCompleted) return;

        if (Input.GetKey(KeyCode.E))
            StartSearching();
        else
            StopSearching();

        if (isSearching)
        {
            currentTime += Time.deltaTime;
            progressBar.value = currentTime / searchTime;

            if (currentTime >= searchTime)
                CompleteSearch();
        }
    }

    void StartSearching()
    {
        isSearching = true;
        progressUI.SetActive(true);
    }

    void StopSearching()
    {
        isSearching = false;
        currentTime = 0f;
        progressBar.value = 0f;
        progressUI.SetActive(false);
    }

    void CompleteSearch()
    {
        Debug.Log("ค้นเสร็จ! ได้กุญแจ");

        // 🔑 ได้กุญแจ
        GameManager.instance.hasKey = true;

        isCompleted = true;
        isSearching = false;
        currentTime = 0f;
        progressBar.value = 0f;
        progressUI.SetActive(false);

        SpawnItemPopup();
    }

    void SpawnItemPopup()
    {
        if (itemPopupPrefab == null) return;

        Vector3 pos = spawnPoint != null ? spawnPoint.position : transform.position;
        GameObject item = Instantiate(itemPopupPrefab, pos, Quaternion.identity);

        StartCoroutine(FloatAndFade(item));
    }

    System.Collections.IEnumerator FloatAndFade(GameObject obj)
    {
        float duration = 1.5f;
        float timer = 0f;

        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();

        while (timer < duration)
        {
            timer += Time.deltaTime;

            obj.transform.position += Vector3.up * Time.deltaTime;

            if (sr != null)
            {
                float alpha = Mathf.Lerp(1f, 0f, timer / duration);
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
            }

            yield return null;
        }

        Destroy(obj);
    }

    // ===== 2D Trigger =====
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("เข้า range");
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            StopSearching();
        }
    }
}