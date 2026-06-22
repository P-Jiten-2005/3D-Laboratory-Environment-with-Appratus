using UnityEngine;

public class BeakLiquid : MonoBehaviour
{
    [Range(0f, 1f)]
    public float fillAmount = 0.5f;

    public Transform liquid;

    public float minY = -0.05f;
    public float maxY = 0.05f;

    private void Update()
    {
        if (liquid == null)
            return;

        Vector3 pos = liquid.localPosition;
        pos.y = Mathf.Lerp(minY, maxY, fillAmount);
        liquid.localPosition = pos;
    }
}