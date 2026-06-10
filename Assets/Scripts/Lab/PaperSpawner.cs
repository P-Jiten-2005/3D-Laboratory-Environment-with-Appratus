using UnityEngine;

public class PaperSpawner : MonoBehaviour
{
    public GameObject paperPrefab;
    public Transform spawnPoint;

    private GameObject currentPaper;

    public void SpawnPaper()
    {
        if (currentPaper != null)
        {
            return;
        }

        currentPaper = Instantiate(
            paperPrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );
    }

    public void ClearPaper()
    {
        currentPaper = null;
    }
}