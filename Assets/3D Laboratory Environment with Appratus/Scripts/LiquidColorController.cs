using UnityEngine;

public class LiquidColorController : MonoBehaviour
{
    public Renderer liquidRenderer;

    private Color startColor = new Color(1f, 0f, 1f);
    private Color endColor = Color.white;

    void Start()
    {
        if (liquidRenderer != null)
        {
            liquidRenderer.material.color = startColor;
        }
    }

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