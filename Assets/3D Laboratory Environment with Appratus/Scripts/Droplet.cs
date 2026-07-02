using UnityEngine;

public class Droplet : MonoBehaviour
{
    public bool isReleased = false;

    public void Release()
    {
        if (isReleased) return;

        isReleased = true;

        transform.parent = null;

        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = true;
        rb.mass = 0.001f;
        rb.linearDamping = 2f;
    }
}