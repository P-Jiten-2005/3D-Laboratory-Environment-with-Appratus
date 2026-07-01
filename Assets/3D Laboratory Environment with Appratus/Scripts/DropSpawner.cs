using UnityEngine;

public class DropSpawner : MonoBehaviour
{
    public GameObject acidDropPrefab;
    public float dropInterval = 0.3f;

    private float timer = 0f;
    public bool isSpawning = false;   // 👈 ADD THIS

    void Update()
    {
        if (!isSpawning) return;  // 👈 ADD THIS

        timer += Time.deltaTime;

        if (timer >= dropInterval)
        {
            Instantiate(
                acidDropPrefab,
                transform.position,
                Quaternion.identity
            );

            timer = 0f;
        }
    }

    public void StartSpawning()
    {
        isSpawning = true;
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }
}