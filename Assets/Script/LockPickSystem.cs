using UnityEngine;

public class LockPickSystem : MonoBehaviour
{
    public Transform player;
    public Transform lockPoint;
    public Rigidbody2D playerRigidbody;
    public MonoBehaviour movementComponent;
    public float interactionDistance = 2f;
    public float progress = 0f;
    public float maxProgress = 100f;
    public float successProgressGain = 25f;
    public float failPenalty = 5f;
    public float catchPenalty = 20f;
    public AudioSource audioSource;
    public AudioClip skillCheckAlertSfx;
    public AudioClip successSfx;
    public AudioClip failSfx;
    public AudioClip unlockSfx;
    public GameObject lockpickUI;
    public GameObject objectToUnlock;
    public GameObject enemyToDisable;
    public GameObject encounterClearObject;
    public float clearMessageDuration = 3f;
    public bool disableAfterUnlock = true;

    public float skillCheckInterval = 3f;
    float timer;

    bool canPress = false;
    [Range(0.05f, 0.5f)]
    public float successZone = 0.18f;
    float successZoneStart = 0.4f;
    float currentPos;
    float moveDirection = 1f;
    public float markerSpeed = 1.5f;
    bool encounterActive = false;
    bool movementLocked = false;

    public float ProgressNormalized => maxProgress <= 0f ? 0f : progress / maxProgress;
    public float SkillCheckPositionNormalized => Mathf.Clamp01(currentPos);
    public float SuccessZoneStartNormalized => successZoneStart;
    public float SuccessZoneSizeNormalized => successZone;
    public bool IsSkillCheckActive => canPress;
    public bool IsEncounterActive => encounterActive;

    void Start()
    {
        timer = skillCheckInterval;
        SetUIState(false);
    }

    void Update()
    {
        if (!encounterActive) return;

        HandleFacingOnlyInput();
        movementLocked = canPress;
        ToggleMovementComponent();
        LockPlayerMovement();

        if (!CanAttemptLockpick())
        {
            canPress = false;
            currentPos = 0f;
            moveDirection = 1f;
            movementLocked = false;
            ToggleMovementComponent();
            return;
        }

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            TriggerSkillCheck();
            timer = Random.Range(1f, 2f);
        }

        if (canPress)
        {
            currentPos += Time.deltaTime * markerSpeed * moveDirection;

            if (currentPos >= 1f)
            {
                OnSkillCheckMissed();
                return;
            }
            else if (currentPos <= 0f)
            {
                OnSkillCheckMissed();
                return;
            }

            if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Space))
            {
                CheckResult();
            }
        }
    }

    public void BeginLockpickEncounter()
    {
        progress = 0f;
        timer = skillCheckInterval;
        canPress = false;
        currentPos = 0f;
        moveDirection = 1f;
        encounterActive = true;
        enabled = true;
        SetUIState(true);
        SetMovementLock(false);
    }

    public void OnPlayerCaught()
    {
        if (!encounterActive) return;

        progress = Mathf.Max(progress - catchPenalty, 0f);
        canPress = false;
        timer = skillCheckInterval;
        currentPos = 0f;
        Debug.Log("โดนจับ! ความคืบหน้าลดลง");
    }

    void TriggerSkillCheck()
    {
        canPress = true;
        currentPos = 0f;
        moveDirection = 1f;
        successZoneStart = Random.Range(0.1f, 0.9f - successZone);

        if (audioSource != null && skillCheckAlertSfx != null)
        {
            audioSource.PlayOneShot(skillCheckAlertSfx);
        }
    }

    void CheckResult()
    {
        canPress = false;

        if (currentPos >= successZoneStart && currentPos <= successZoneStart + successZone)
        {
            Debug.Log("Success!");
            progress = Mathf.Min(progress + successProgressGain, maxProgress);
            PlaySfx(successSfx);

            if (progress >= maxProgress)
            {
                UnlockDoor();
            }
        }
        else
        {
            Debug.Log("Fail!");
            progress = Mathf.Max(progress - failPenalty, 0f);
            PlaySfx(failSfx);
        }
    }

    void OnSkillCheckMissed()
    {
        canPress = false;
        currentPos = 0f;
        moveDirection = 1f;
        progress = Mathf.Max(progress - failPenalty, 0f);
        PlaySfx(failSfx);
        Debug.Log("Miss!");
    }

    void UnlockDoor()
    {
        encounterActive = false;
        canPress = false;
        SetUIState(false);
        SetMovementLock(false);
        PlaySfx(unlockSfx);

        if (objectToUnlock != null && disableAfterUnlock)
        {
            objectToUnlock.SetActive(false);
        }

        if (enemyToDisable != null)
        {
            enemyToDisable.SetActive(false);
        }

        if (encounterClearObject != null)
        {
            encounterClearObject.SetActive(true);
            CancelInvoke(nameof(HideClearMessage));
            Invoke(nameof(HideClearMessage), clearMessageDuration);
        }

        Debug.Log("ปลดล็อกสำเร็จ!");
    }

    void SetUIState(bool isVisible)
    {
        if (lockpickUI != null)
        {
            lockpickUI.SetActive(isVisible);
        }
    }

    void HideClearMessage()
    {
        if (encounterClearObject != null)
        {
            encounterClearObject.SetActive(false);
        }
    }

    void PlaySfx(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    bool CanAttemptLockpick()
    {
        if (player == null || lockPoint == null)
        {
            return true;
        }

        Vector2 toLock = lockPoint.position - player.position;

        if (toLock.magnitude > interactionDistance)
        {
            return false;
        }

        float facingDirection = player.localScale.x >= 0f ? 1f : -1f;
        return Mathf.Sign(toLock.x) == facingDirection || Mathf.Abs(toLock.x) < 0.2f;
    }

    void SetMovementLock(bool shouldLock)
    {
        movementLocked = shouldLock;
        ToggleMovementComponent();

        if (playerRigidbody != null)
        {
            playerRigidbody.linearVelocity = Vector2.zero;
        }
    }

    void ToggleMovementComponent()
    {
        if (movementComponent != null)
        {
            movementComponent.enabled = !movementLocked;
        }
    }

    void LockPlayerMovement()
    {
        if (!movementLocked || playerRigidbody == null) return;

        playerRigidbody.linearVelocity = new Vector2(0f, playerRigidbody.linearVelocity.y);
    }

    void HandleFacingOnlyInput()
    {
        if (!movementLocked || player == null) return;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            SetFacingDirection(-1f);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            SetFacingDirection(1f);
        }
    }

    void SetFacingDirection(float direction)
    {
        Vector3 scale = player.localScale;
        scale.x = Mathf.Abs(scale.x) * Mathf.Sign(direction);
        player.localScale = scale;
    }
}
