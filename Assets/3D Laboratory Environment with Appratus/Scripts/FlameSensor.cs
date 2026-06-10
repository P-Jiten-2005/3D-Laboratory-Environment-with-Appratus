using UnityEngine;

public class FlameSensor : MonoBehaviour
{
    public FlameController flame;

    private void OnTriggerStay(Collider other)
    {
        SpoonController spoon =
            other.GetComponent<SpoonController>();

        if (spoon == null) return;

        if (spoon.calciumSalt.activeSelf)
            flame.SetCalcium();

        else if (spoon.bariumSalt.activeSelf)
            flame.SetBarium();

        else if (spoon.potassiumSalt.activeSelf)
            flame.SetPotassium();
    }

    private void OnTriggerExit(Collider other)
    {
        SpoonController spoon =
            other.GetComponent<SpoonController>();

        if (spoon == null) return;

        flame.ResetFlame();

        spoon.ClearSalt();
    }
}