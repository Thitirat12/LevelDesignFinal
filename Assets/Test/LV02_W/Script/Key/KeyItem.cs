using UnityEngine;

public enum KeyType
{
    Red,
    Blue,
    Yellow
}

public class KeyItem : MonoBehaviour
{
    public KeyType keyType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerInventory inv = other.GetComponent<PlayerInventory>();

        if (inv != null)
        {
            switch (keyType)
            {
                case KeyType.Red:
                    inv.hasRedKey = true;
                    break;

                case KeyType.Blue:
                    inv.hasBlueKey = true;
                    break;

                case KeyType.Yellow:
                    inv.hasYellowKey = true;
                    break;
            }

            Debug.Log("Picked up: " + keyType);

            Destroy(gameObject);
        }
    }
}
