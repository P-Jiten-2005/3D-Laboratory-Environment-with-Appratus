using UnityEngine;

public class TitrationManager : MonoBehaviour
{
    public static TitrationManager Instance;

    public float currentVolume = 0f;
    public float volumePerDrop = 0.1f;

    public float endpointVolume = 6f;

    private bool endpointReached = false;

    private void Awake()
    {
        Instance = this;
    }

    public void AddDrop()
    {
        currentVolume += volumePerDrop;

        if (!endpointReached &&
           currentVolume >= endpointVolume)
        {
            endpointReached = true;

            Debug.Log("ENDPOINT REACHED!");
        }
    }
}