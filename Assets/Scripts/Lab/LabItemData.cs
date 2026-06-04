using UnityEngine;

// Attach this to every interactable chemical object in the lab.
// It's just a data holder — no logic, just information.
public class LabItemData : MonoBehaviour
{
    // What chemical is this? e.g. "NaOH", "Water", "LemonJuice"
    public string chemicalName;

    // The pH value of this chemical (0-14)
    public float pHValue;

    // What color should this chemical's liquid appear?
    public Color liquidColor = Color.blue;

    // Is there liquid currently in this container?
    public bool isFilled = true;
}
