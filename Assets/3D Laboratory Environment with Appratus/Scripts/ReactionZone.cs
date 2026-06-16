using UnityEngine;

public class ReactionZone : MonoBehaviour
{
    public int dropCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AcidDrop"))
        {
            Debug.Log("DESTROYING: " + other.name);

            dropCount++;

            TitrationManager.Instance.AddDrop();

            Destroy(other.gameObject);
        }
    }
}