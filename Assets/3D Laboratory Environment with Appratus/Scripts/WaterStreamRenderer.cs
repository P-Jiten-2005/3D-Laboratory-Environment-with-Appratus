using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class WaterStreamRenderer : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;

    public int segments = 20;

    private LineRenderer lr;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        DrawStream();
    }

    void DrawStream()
    {
        lr.positionCount = segments;

        Vector3 start = startPoint.position;
        Vector3 end = endPoint.position;

        for (int i = 0; i < segments; i++)
        {
            float t = i / (float)(segments - 1);

            Vector3 pos = Vector3.Lerp(start, end, t);

            // Creates a gravity-like curve
            pos.y -= Mathf.Sin(t * Mathf.PI) * 0.2f;

            lr.SetPosition(i, pos);
        }
    }
}