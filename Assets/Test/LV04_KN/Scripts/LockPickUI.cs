using UnityEngine;
using UnityEngine.UI;

public class LockPickUI : MonoBehaviour
{
    public LockPickSystem lockpickSystem;
    public Slider progressSlider;
    public RectTransform skillCheckArea;
    public RectTransform marker;
    public RectTransform successZone;

    void Update()
    {
        if (lockpickSystem == null) return;

        if (progressSlider != null)
        {
            progressSlider.value = lockpickSystem.ProgressNormalized;
        }

        if (skillCheckArea == null) return;

        float width = skillCheckArea.rect.width;

        if (successZone != null)
        {
            successZone.anchoredPosition = new Vector2(width * lockpickSystem.SuccessZoneStartNormalized, successZone.anchoredPosition.y);
            successZone.SetSizeWithCurrentAnchors(
                RectTransform.Axis.Horizontal,
                width * lockpickSystem.SuccessZoneSizeNormalized
            );
            successZone.gameObject.SetActive(lockpickSystem.IsSkillCheckActive);
        }

        if (marker != null)
        {
            marker.anchoredPosition = new Vector2(width * lockpickSystem.SkillCheckPositionNormalized, marker.anchoredPosition.y);
            marker.gameObject.SetActive(lockpickSystem.IsSkillCheckActive);
        }
    }
}
