using UnityEngine;

public class TapController : MonoBehaviour
{
    public GameObject waterStream;

    bool waterOn = false;

    public void ToggleWater()
    {
        waterOn = !waterOn;

        waterStream.SetActive(waterOn);

        if (waterOn)
        {
            transform.localRotation =
                Quaternion.Euler(0, 0, -90);
        }
        else
        {
            transform.localRotation =
                Quaternion.Euler(0, 0, 0);
        }
    }
}