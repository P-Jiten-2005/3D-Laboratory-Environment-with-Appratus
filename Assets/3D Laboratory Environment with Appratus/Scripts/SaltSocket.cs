using UnityEngine;

public class SaltSocket : MonoBehaviour
{
    public enum SaltKind
    {
        Calcium,
        Barium,
        Potassium
    }

    public SaltKind saltType;

    private void OnTriggerEnter(Collider other)
    {
        SpoonController spoon =
            other.GetComponent<SpoonController>();

        if (spoon == null) return;

        switch (saltType)
        {
            case SaltKind.Calcium:
                spoon.SetCalcium();
                break;

            case SaltKind.Barium:
                spoon.SetBarium();
                break;

            case SaltKind.Potassium:
                spoon.SetPotassium();
                break;
        }
    }
}