using UnityEngine;

public class WallSwitch : Interacable
{
    public GameObject wall;
    private bool isOn = false;

    public override void Interact()
    {
        isOn = !isOn;

        wall.SetActive(!isOn);

        Debug.Log(isOn ? "Wall Opened" : "Wall Closed");
    }
}
