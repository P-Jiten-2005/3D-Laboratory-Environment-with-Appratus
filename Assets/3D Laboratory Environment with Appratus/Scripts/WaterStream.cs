using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class WaterStream : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;

    private LineRenderer lr;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();

        lr.positionCount = 2;
        lr.enabled = false;
    }

    private void Update()
    {
        if (!lr.enabled)
            return;

        if (startPoint == null || endPoint == null)
            return;

        lr.SetPosition(0, startPoint.position);
        lr.SetPosition(1, endPoint.position);
    }

    public void ShowStream()
    {
        lr.enabled = true;
    }

    public void HideStream()
    {
        lr.enabled = false;
    }
}