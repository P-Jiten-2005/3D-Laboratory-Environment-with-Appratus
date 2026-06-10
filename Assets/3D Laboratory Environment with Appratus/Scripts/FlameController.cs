using UnityEngine;

public class FlameController : MonoBehaviour
{
    ParticleSystem ps;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();

        Debug.Log("PS = " + ps);
    }

    public void SetPotassium()
    {
        Debug.Log("SetPotassium");

        if (ps == null)
        {
            Debug.LogError("PS NULL");
            return;
        }

        var main = ps.main;
        main.startColor = new Color(0.85f, 0.65f, 1.0f);
    }

    public void SetBarium()
    {
        Debug.Log("SetBarium");

        if (ps == null)
        {
            Debug.LogError("PS NULL");
            return;
        }

        var main = ps.main;
        main.startColor = new Color(0.6f, 1.0f, 0.2f);
    }

    public void SetCalcium()
    {
        Debug.Log("SetCalcium");

        if (ps == null)
        {
            Debug.LogError("PS NULL");
            return;
        }

        var main = ps.main;
        main.startColor = new Color(0.67f, 0.29f, 0.27f);
    }

    public void ResetFlame()
    {
        Debug.Log("ResetFlame");

        if (ps == null)
        {
            Debug.LogError("PS NULL");
            return;
        }

        var main = ps.main;

        main.startColor = new Color(
            11f / 255f,
            73f / 255f,
            184f / 255f
        );
    }
}