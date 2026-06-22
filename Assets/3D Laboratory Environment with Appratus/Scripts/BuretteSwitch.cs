using UnityEngine;

public class BuretteSwitch : MonoBehaviour
{
    public static bool IsOpen = false;

    public void ToggleSwitch()
    {
        IsOpen = !IsOpen;

        Debug.Log("Switch = " + IsOpen);
    }
}