using UnityEngine;

public class DropSpawner : MonoBehaviour
{
    public GameObject acidDropPrefab;
    public float dropInterval = 0.3f;

    private float timer = 0f;

    void Update()
    {
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
}