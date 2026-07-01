using UnityEngine;

public class TestTubePH : MonoBehaviour
{
    [Range(0, 14)]
    public float pH = 7;

    public Renderer liquidRenderer;

    public void AddIndicator()
    {
        if (liquidRenderer == null)
            return;

        Material mat = liquidRenderer.material;

        Color targetColor = GetPHColor();

        mat.SetColor("_OutsideColor", targetColor);
        mat.SetColor("_InsideColor", targetColor);
    }

    Color GetPHColor()
    {
        if (pH <= 2)
            return new Color(0.56f, 0f, 1f); // Violet (V)

        if (pH <= 4)
            return new Color(0.29f, 0f, 0.51f); // Indigo (I)

        if (pH <= 6)
            return Color.blue; // Blue (B)

        if (pH <= 7)
            return Color.green; // Green (G)

        if (pH <= 8)
            return Color.yellow; // Yellow (Y)

        if (pH <= 10)
            return new Color(1f, 0.5f, 0f); // Orange (O)

        return Color.red; // Red (R)
    }
}