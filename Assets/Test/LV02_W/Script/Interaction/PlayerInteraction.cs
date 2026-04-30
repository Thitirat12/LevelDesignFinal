using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactRange = 2f;
    public Vector2 interactRadius;
    public LayerMask interactLayer;

    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            Debug.Log("E pressed");
            TryInteract();
        }
    }

    void TryInteract()
    {
        Vector2 dir = Vector2.right;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, interactRange, interactLayer);


        if (hit.collider == null)
        {
            Debug.Log("Nothing hit");
            return;
        }

        Debug.Log("Found: " + hit.collider.name);

        Interacable obj = hit.collider.GetComponent<Interacable>();

        if (obj != null)
        {
            obj.Interact();
        }
    }
}
