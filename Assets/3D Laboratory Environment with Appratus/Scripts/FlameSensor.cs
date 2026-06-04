using UnityEngine;

public class FlameSensor : MonoBehaviour
{
    public FlameController flame;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered flame: " + other.name);

        SpoonController spoon =
            other.GetComponent<SpoonController>();

        if (spoon == null)
        {
            Debug.Log("Not a spoon");
            return;
        }

        Debug.Log("Spoon detected");

        if (spoon.calciumSalt.activeSelf)
        {
            Debug.Log("Calcium detected");
            flame.SetCalcium();
        }
        else if (spoon.bariumSalt.activeSelf)
        {
            Debug.Log("Barium detected");
            flame.SetBarium();
        }
        else if (spoon.potassiumSalt.activeSelf)
        {
            Debug.Log("Potassium detected");
            flame.SetPotassium();
        }
        else
        {
            Debug.Log("No salt active");
        }
    }
}