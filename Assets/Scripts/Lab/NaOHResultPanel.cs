using UnityEngine;

public class NaOHResultPanel : MonoBehaviour
{
    public void ShowPanel()
    {
        Debug.Log("SHOW PANEL CALLED");
        gameObject.SetActive(true);
    }
}