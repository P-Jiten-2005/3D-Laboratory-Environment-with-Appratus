using UnityEngine;

public class TitrationManager : MonoBehaviour
{
    public static TitrationManager Instance;

    [Header("Volume Settings")]
    public float currentVolume = 0f;
    public float volumePerDrop = 0.1f;
    public float endpointVolume = 6f;

    [Header("Popup Reference")]
    public ResultPopup popup;

    private bool endpointReached = false;

    private void Awake()
    {
        Instance = this;
    }

    // 👇 Call this from ReactionZone when drop hits flask
    public void AddDrop()
    {
        if (endpointReached) return;

        currentVolume += volumePerDrop;

        Debug.Log("Current Volume: " + currentVolume);

        if (currentVolume >= endpointVolume)
        {
            endpointReached = true;

            Debug.Log("ENDPOINT REACHED!");

            if (popup != null)
            {
                popup.ShowResult(endpointVolume);
            }
            else
            {
                Debug.LogWarning("Popup NOT assigned in Inspector!");
            }
        }
    }

    // Optional reset (future use)
    public void ResetTitration()
    {
        currentVolume = 0f;
        endpointReached = false;
    }
}