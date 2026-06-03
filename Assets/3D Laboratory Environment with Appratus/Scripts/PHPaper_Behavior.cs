using UnityEngine;

// Attach to PHPaper_Strip.
// This script watches for when the strip is dipped into a chemical.
public class PHPaper_Behavior : MonoBehaviour
{
    // Drag the paper's Renderer (MeshRenderer) here in the Inspector
    public Renderer paperRenderer;

    // Track if paper was already used (once dipped, don't change again)
    private bool alreadyDipped = false;

    // Unity calls this automatically when this object enters a trigger collider
    void OnTriggerEnter(Collider other)
    {
        // Don't react if already used
        if (alreadyDipped) return;

        // Check if the trigger we entered belongs to an object with LabItemData
        alreadyDipped = true;

        paperRenderer.material.color = Color.magenta;
    }

    // Returns the color the paper should turn for a given pH
    Color GetPHColor(float pH)
    {
        if (pH <= 3) return new Color(0.9f, 0.1f, 0.1f);  // Red   — strong acid
        if (pH <= 6) return new Color(1.0f, 0.5f, 0.0f);  // Orange — weak acid
        if (pH == 7) return new Color(0.2f, 0.8f, 0.2f);  // Green  — neutral
        if (pH <= 9) return new Color(0.2f, 0.5f, 1.0f);  // Blue   — weak base
        return new Color(0.6f, 0.0f, 0.8f);                // Violet — strong base
    }
}
