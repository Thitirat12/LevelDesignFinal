using UnityEditor.EditorTools;
using UnityEngine;

public class LockObject : Interacable
{
    public KeyType requiredKey;
    public DoorManager doorManager;

    private bool isUnlocked = false;

    public override void Interact()
    {
        if (isUnlocked)
        {
            Debug.Log("Already unlocked");
            return;
        }

        Debug.Log("LOCK NAME: " + gameObject.name);
        Debug.Log("REQUIRED KEY: " + requiredKey);

        PlayerInventory inv = FindFirstObjectByType<PlayerInventory>();

        Debug.Log("Try unlock: " + requiredKey);

        if (!inv.HasKey(requiredKey))
        {
            Debug.Log("Missing key: " + requiredKey);
            return;
        }

        inv.UseKey(requiredKey);

        isUnlocked = true;

        doorManager.RegisterUnlock(requiredKey);

        Debug.Log("Unlocked: " + requiredKey);
    }
}
