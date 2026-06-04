using UnityEngine;

public class Spoon : MonoBehaviour
{
    public GameObject saltOnSpoon;

    public string currentChemical = "";

    public void PickUpSalt(string chemical)
    {
        currentChemical = chemical;

        saltOnSpoon.SetActive(true);
    }
}