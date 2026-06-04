using UnityEngine;

public class SaltPile : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Spoon spoon = other.GetComponent<Spoon>();

        if(spoon != null)
        {
            spoon.PickUpSalt("Sodium");
        }
    }
}