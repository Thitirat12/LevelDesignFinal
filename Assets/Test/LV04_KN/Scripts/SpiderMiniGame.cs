using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class SpiderMiniGame : MonoBehaviour
{
    [Header("UI")]
    public GameObject promptPanel;
    [Tooltip("ปุ่มด้านหน้า: ผู้เล่นต้องกดปุ่มนี้ก่อน")]
    public TextMeshProUGUI firstKeyText;
    [Tooltip("ปุ่มด้านหลัง: ผู้เล่นต้องกดปุ่มนี้ทีหลัง")]
    public TextMeshProUGUI secondKeyText;

    [Header("Result")]
    public LockPickSystem lockpickSystem;
    public SpiderDropper spiderDropper;
    public float progressPenalty = 20f;
    public bool resetSpiderOnFinish = true;
    public UnityEvent onSuccess;
    public UnityEvent onFail;

    [Header("Settings")]
    public float timeLimit = 3f;
    public bool hideOnWrongKey = false;
    public bool useSecondTextAsFront = false;

    KeyCode[] keyChoices = { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };
    KeyCode[] currentKeys = new KeyCode[2];
    int currentIndex = 0;
    float timer = 0f;
    bool isActive = false;

    void Start()
    {
        SetUIState(false);
    }

    void Update()
    {
        if (!isActive) return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            FailMiniGame();
            return;
        }

        CheckKey(KeyCode.W);
        CheckKey(KeyCode.A);
        CheckKey(KeyCode.S);
        CheckKey(KeyCode.D);
    }

    public void StartSpiderMiniGame()
    {
        currentKeys[0] = GetRandomKey();
        currentKeys[1] = GetRandomKey();
        currentIndex = 0;
        timer = timeLimit;
        isActive = true;

        UpdatePromptUI();
        SetUIState(true);

        Debug.Log("Spider mini game started");
    }

    public void CancelSpiderMiniGame()
    {
        isActive = false;
        ClearPromptUI();
        SetUIState(false);
    }

    public bool IsActive => isActive;

    void CheckKey(KeyCode key)
    {
        if (!Input.GetKeyDown(key)) return;

        if (key == currentKeys[currentIndex])
        {
            currentIndex++;
            UpdatePromptUI();

            if (currentIndex >= currentKeys.Length)
            {
                CompleteMiniGame();
            }
        }
        else
        {
            Debug.Log("Wrong spider key");

            if (hideOnWrongKey)
            {
                FailMiniGame();
            }
        }
    }

    KeyCode GetRandomKey()
    {
        return keyChoices[Random.Range(0, keyChoices.Length)];
    }

    void UpdatePromptUI()
    {
        if (firstKeyText != null)
        {
            firstKeyText.text = GetDisplayTextForSlot(0);
            firstKeyText.color = IsSlotCompleted(0) ? Color.gray : Color.white;
        }

        if (secondKeyText != null)
        {
            secondKeyText.text = GetDisplayTextForSlot(1);
            secondKeyText.color = IsSlotCompleted(1) ? Color.gray : Color.white;
        }
    }

    string GetDisplayTextForSlot(int textSlotIndex)
    {
        int keyIndex = GetKeyIndexForSlot(textSlotIndex);

        if (currentIndex > keyIndex)
        {
            return "";
        }

        return GetKeyDisplayText(keyIndex);
    }

    bool IsSlotCompleted(int textSlotIndex)
    {
        return currentIndex > GetKeyIndexForSlot(textSlotIndex);
    }

    int GetKeyIndexForSlot(int textSlotIndex)
    {
        int keyIndex = textSlotIndex;

        if (useSecondTextAsFront)
        {
            keyIndex = textSlotIndex == 0 ? 1 : 0;
        }

        return keyIndex;
    }

    string GetKeyDisplayText(int index)
    {
        if (index < 0 || index >= currentKeys.Length)
        {
            return "";
        }

        return currentKeys[index].ToString();
    }

    void CompleteMiniGame()
    {
        isActive = false;
        ClearPromptUI();
        SetUIState(false);

        if (resetSpiderOnFinish && spiderDropper != null)
        {
            spiderDropper.ResetSpider();
        }

        onSuccess?.Invoke();
        Debug.Log("Spider mini game success");
    }

    void FailMiniGame()
    {
        isActive = false;
        ClearPromptUI();
        SetUIState(false);

        if (lockpickSystem != null)
        {
            lockpickSystem.ReduceProgress(progressPenalty);
        }

        if (resetSpiderOnFinish && spiderDropper != null)
        {
            spiderDropper.ResetSpider();
        }

        onFail?.Invoke();
        Debug.Log("Spider mini game failed");
    }

    void SetUIState(bool isVisible)
    {
        if (promptPanel != null)
        {
            promptPanel.SetActive(isVisible);
        }
    }

    void ClearPromptUI()
    {
        if (firstKeyText != null)
        {
            firstKeyText.text = "";
        }

        if (secondKeyText != null)
        {
            secondKeyText.text = "";
        }
    }
}
