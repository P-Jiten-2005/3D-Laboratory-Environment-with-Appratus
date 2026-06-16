using UnityEngine;

public class TitrationManager : MonoBehaviour
{
    public static TitrationManager Instance;

    public float currentVolume = 0f;
    public float volumePerDrop = 0.1f;

    private void Awake()
    {
        Instance = this;
    }

    public void AddDrop()
    {
        currentVolume += volumePerDrop;

        Debug.Log("Volume: " + currentVolume.ToString("F1") + " mL");
    }
}