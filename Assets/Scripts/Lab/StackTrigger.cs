using UnityEngine;

public class StackTrigger : MonoBehaviour
{
    public PaperSpawner spawner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RightHand"))
        {
            spawner.SpawnPaper();
        }
    }
}