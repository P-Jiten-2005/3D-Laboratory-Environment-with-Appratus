using UnityEngine;
using TMPro;

public class ResultPopup : MonoBehaviour
{
    public GameObject panel;
    public TMP_Text resultText;

    public void ShowResult(float endpointVolume)
    {
        panel.SetActive(true);

        resultText.text =
            "Titration Complete\n" +
            "End point observed at " + endpointVolume + " ml";
    }
}