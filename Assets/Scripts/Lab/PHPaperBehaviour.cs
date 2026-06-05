using UnityEngine;

// Attach to PHPaper_Strip. 
public class PHPaperBehaviour : MonoBehaviour
{
    private Renderer paperRenderer;

    private bool alreadyDipped = false;

    void Awake()
    {
        paperRenderer = GetComponent<Renderer>();

        Debug.Log("Renderer found: " + paperRenderer);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Paper touched: " + other.name);

        if (alreadyDipped) return;

        var chemData = other.GetComponentInParent<LabItemData>();

        if (chemData == null)
        {
            Debug.Log("No LabItemData found");
            return;
        }

        Debug.Log("Chemical found: " + chemData.chemicalName);
        Debug.Log("pH = " + chemData.pHValue);

        alreadyDipped = true;

        paperRenderer.material.color = GetPHColor(chemData.pHValue);
    }

    Color GetPHColor(float pH)
    {
        if (pH <= 3)
            return new Color(0.9f, 0.1f, 0.1f);

        if (pH <= 6)
            return new Color(1.0f, 0.5f, 0.0f);

        if (pH == 7)
            return new Color(0.2f, 0.8f, 0.2f);

        if (pH <= 9)
            return new Color(0.2f, 0.5f, 1.0f);

        return new Color(0.6f, 0.0f, 0.8f);
    }
}