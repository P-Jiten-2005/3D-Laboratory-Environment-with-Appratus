using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class WaterStreamRenderer : MonoBehaviour
{
    [Header("Stream Points")]
    public Transform startPoint;
    public Transform endPoint;

    [Header("Source Liquid")]
    public BeakerLiquid sourceLiquid;

    [Header("Stream Shape")]
    public int segments = 20;
    public float curveAmount = 0.2f;

    [Header("Width Settings")]
    public float minWidth = 0.005f;
    public float maxWidth = 0.03f;

    private LineRenderer lr;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();

        lr.useWorldSpace = true;
        lr.positionCount = segments;
    }

    void Update()
    {
        if (startPoint == null || endPoint == null)
            return;

        if (sourceLiquid == null)
            return;

        UpdateWidth();
        DrawStream();
    }

    void UpdateWidth()
    {
        float width = Mathf.Lerp(
            minWidth,
            maxWidth,
            sourceLiquid.fillAmount
        );

        AnimationCurve widthCurve = new AnimationCurve();

        widthCurve.AddKey(0f, width);
        widthCurve.AddKey(1f, width * 0.5f);

        lr.widthCurve = widthCurve;
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

            // Gravity-like curve
            pos.y -= Mathf.Sin(t * Mathf.PI) * curveAmount;

            lr.SetPosition(i, pos);
        }
    }
}