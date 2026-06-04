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
        main.startColor = new Color(0.8f, 0.4f, 1f);
    }

    public void SetBarium()
    {
        var main = ps.main;
        main.startColor = Color.green;
    }

    public void SetCalcium()
    {
        var main = ps.main;
        main.startColor = new Color(1f, 0.4f, 0.2f);
    }

    public void ResetFlame()
    {
        var main = ps.main;
        main.startColor = Color.white;
    }
}