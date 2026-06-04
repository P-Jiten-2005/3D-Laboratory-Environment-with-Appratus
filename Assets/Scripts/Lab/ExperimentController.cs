 
using UnityEngine;
using TMPro;  
 
// This is a BASE CLASS — you never attach this directly to a GameObject.
// Your experiment scripts (NaOHExperiment, etc.) inherit from this.
public abstract class ExperimentController : MonoBehaviour
{
    public GameObject resultPanel;
 
    public TextMeshProUGUI pHText;
    public TextMeshProUGUI resultText;
 
    public abstract void OnItemSocketed(LabItemData item, string socketTag);
 
    // Call this when the experiment is complete to show the result.
    // name    = chemical name (e.g. "NaOH")
    // pH      = pH value (e.g. 13f)
    // nature  = "Strong Base", "Neutral", "Strong Acid" etc.
    // color   = color for the result text
    protected void ShowResult(string name, float pH, string nature, Color color)
    {
        resultPanel.SetActive(true);   
        pHText.text    = $"pH = {pH:F1}";
        resultText.text  = $"{name} → {nature}";
        resultText.color = color;
    }
}
