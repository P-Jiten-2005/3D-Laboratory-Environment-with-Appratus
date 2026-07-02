using UnityEngine;
using TMPro;

public class ResultPopup : MonoBehaviour
{
    public GameObject panel;
    public TMP_Text resultText;

    private bool shown = false;

    public void ShowResult(float endpointVolume)
    {
        if (shown) return;   // 🔥 prevents repeat popup
        shown = true;

        panel.SetActive(true);

        resultText.text =
            "Titration Complete\n" +
            "End point observed at " + endpointVolume + " ml";
    }
}