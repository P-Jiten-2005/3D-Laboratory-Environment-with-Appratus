using UnityEngine;

public class LiquidColorController : MonoBehaviour
{
    public Renderer liquidRenderer;

    public Color startColor =
        new Color(1f, 0.4f, 0.8f, 0.5f);

    public Color endColor =
        new Color(1f, 1f, 1f, 0.15f);

    void Update()
    {
        float current =
            TitrationManager.Instance.currentVolume;

        float endpoint =
            TitrationManager.Instance.endpointVolume;

        float t = Mathf.Clamp01(current / endpoint);

        liquidRenderer.material.SetColor(
            "_BaseColor",
            Color.Lerp(startColor, endColor, t)
        );
    }
}