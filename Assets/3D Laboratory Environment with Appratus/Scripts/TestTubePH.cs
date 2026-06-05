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
            return Color.red;

        if (pH <= 4)
            return new Color(1f, 0.2f, 0.6f);

        if (pH <= 6)
            return new Color(0.6f, 0f, 1f);

        if (pH <= 7)
            return Color.blue;

        if (pH <= 9)
            return Color.cyan;

        if (pH <= 11)
            return Color.green;

        return Color.yellow;
    }
}