using UnityEngine;

public class WarningTrigger : MonoBehaviour
{
    public GameObject warningText;
    public float showTime = 2f;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            StartCoroutine(ShowWarning());
        }
    }

    System.Collections.IEnumerator ShowWarning()
    {
        warningText.SetActive(true);

        yield return new WaitForSeconds(showTime);

        warningText.SetActive(false);
    }
}