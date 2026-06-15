using UnityEngine;

public class DropSpawner : MonoBehaviour
{
    public GameObject acidDropPrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(
                acidDropPrefab,
                transform.position,
                Quaternion.identity
            );
        }
    }
}