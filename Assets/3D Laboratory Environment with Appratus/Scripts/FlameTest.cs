using UnityEngine;
using System.Collections;

public class FlameTest : MonoBehaviour
{
    public ParticleSystem flame;

    private ParticleSystem.MainModule main;

    private void Start()
    {
        main = flame.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        Spoon spoon = other.GetComponent<Spoon>();

        if(spoon == null) return;

        switch(spoon.currentChemical)
        {
            case "Sodium":
                main.startColor = Color.yellow;
                StartCoroutine(BurnSalt(spoon));
                break;
        }
    }

    IEnumerator BurnSalt(Spoon spoon)
    {
        yield return new WaitForSeconds(3);

        spoon.saltOnSpoon.SetActive(false);
        spoon.currentChemical = "";
    }
}