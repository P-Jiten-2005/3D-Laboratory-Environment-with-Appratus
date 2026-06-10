using UnityEngine;

// Attach to PHPaper
public class PHPaperBehaviour : MonoBehaviour
{
    private Renderer paperRenderer;

    // Drag NaOH_ResultPanel here in Inspector

    private bool alreadyDipped = false;

    void Awake()
    {
        paperRenderer = GetComponent<Renderer>();

        Debug.Log("Renderer Found: " + paperRenderer);
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

        Debug.Log("Chemical Found: " + chemData.chemicalName);
        Debug.Log("pH = " + chemData.pHValue);

        alreadyDipped = true;

        // Change paper color
        paperRenderer.material.color = GetPHColor(chemData.pHValue);

        if (chemData.resultPanel != null)
        {
            chemData.resultPanel.ShowPanel();
        }
        else
        {
            Debug.LogWarning("No Result Panel assigned in LabItemData");
        }
        Invoke(nameof(RemovePaper), 5f);
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
    void RemovePaper()
    {
        PaperSpawner spawner =
            FindFirstObjectByType<PaperSpawner>();

        if (spawner != null)
        {
            spawner.ClearPaper();
        }

        Destroy(gameObject);
    }
}