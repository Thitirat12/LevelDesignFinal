using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool hasRedKey;
    public bool hasBlueKey;
    public bool hasYellowKey;

    public bool HasKey(KeyType type)
    {
        switch (type)
        {
            case KeyType.Red: return hasRedKey;
            case KeyType.Blue: return hasBlueKey;
            case KeyType.Yellow: return hasYellowKey;
        }
        return false;
    }

    public void UseKey(KeyType type)
    {
        switch (type)
        {
            case KeyType.Red: hasRedKey = false; break;
            case KeyType.Blue: hasBlueKey = false; break;
            case KeyType.Yellow: hasYellowKey = false; break;
        }
    }
}
