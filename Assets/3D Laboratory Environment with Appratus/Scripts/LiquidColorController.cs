using UnityEngine;

public class LiquidColorController : MonoBehaviour
{
    public Renderer liquidRenderer;

    private Material liquidMaterial;

    private Color startColor = new Color(1f, 0f, 1f);
    private Color endColor = Color.white;

    void Start()
    {
        liquidMaterial = liquidRenderer.material;
    }

    void Update()
    {
        if (TitrationManager.Instance == null)
            return;

        float currentVolume = TitrationManager.Instance.currentVolume;
        float endpointVolume = TitrationManager.Instance.endpointVolume;

        float t = Mathf.Clamp01(currentVolume / endpointVolume);

        Color currentColor = Color.Lerp(startColor, endColor, t);

        liquidMaterial.color = currentColor;

        Debug.Log("Current Volume = " + currentVolume);
        Debug.Log("Current Color = " + currentColor);
    }
}