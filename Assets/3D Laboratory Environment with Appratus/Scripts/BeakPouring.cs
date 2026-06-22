using UnityEngine;

public class BeakPouring : MonoBehaviour
{
    public float pourAngle = 50f;

    public bool IsPouring()
    {
        float angle = Vector3.Angle(transform.up, Vector3.up);

        return angle > pourAngle;
    }
}