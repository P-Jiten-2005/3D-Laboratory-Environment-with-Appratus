using UnityEngine;

public class LiquidColorController : MonoBehaviour
{
    public Renderer liquidRenderer;

    private Color startColor = new Color(1f, 0f, 1f); // Pink
    private Color endColor = Color.white;             // Colorless approximation

    void Update()
    {
        if (TitrationManager.Instance == null)
            return;

        float currentVolume = TitrationManager.Instance.currentVolume;
        float endpointVolume = TitrationManager.Instance.endpointVolume;

        float t = Mathf.Clamp01(currentVolume / endpointVolume);

        Color currentColor = Color.Lerp(startColor, endColor, t);

        liquidRenderer.material.color = currentColor;
    }
}