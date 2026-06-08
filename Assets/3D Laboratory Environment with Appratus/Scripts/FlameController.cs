using UnityEngine;

public class FlameController : MonoBehaviour
{
    ParticleSystem ps;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    public void SetPotassium()
    {
        var main = ps.main;

        // Lilac
        main.startColor = new Color(0.85f, 0.65f, 1.0f);
    }

    public void SetBarium()
    {
        var main = ps.main;

        // Apple Green
        main.startColor = new Color(0.6f, 1.0f, 0.2f);
    }

    public void SetCalcium()
    {
        var main = ps.main;

        // Bright Orange-Red
        main.startColor = new Color(0.67f, 0.29f, 0.27f);
    }

    public void ResetFlame()
    {
        var main = ps.main;

        // Original burner blue
        main.startColor = new Color(
            11f / 255f,
            73f / 255f,
            184f / 255f
        );
    }
}