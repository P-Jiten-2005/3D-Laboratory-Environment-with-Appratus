using UnityEngine;

public class TitrationSwitch : MonoBehaviour
{
    public DropSpawner spawner;

    private bool isOn = false;

    public void ToggleSwitch()
    {
        isOn = !isOn;

        Debug.Log("Switch State: " + isOn);

        if (isOn)
        {
            spawner.StartSpawning();
        }
        else
        {
            spawner.StopSpawning();
        }
    }
}