using UnityEngine;

public class DropSpawner : MonoBehaviour
{
    public GameObject acidDropPrefab;

    public float dropInterval = 1f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= dropInterval)
        {
            Debug.Log("Spawning Drop");

            Instantiate(
                acidDropPrefab,
                transform.position,
                Quaternion.identity
            );

            timer = 0f;
        }
    }
}