using UnityEngine;

public class ReactionZone : MonoBehaviour
{
    public int dropCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AcidDrop"))
        {
            dropCount++;

            Debug.Log("Drop Count: " + dropCount);

            Destroy(other.gameObject);
        }
    }
}