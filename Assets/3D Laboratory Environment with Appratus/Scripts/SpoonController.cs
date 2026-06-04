using UnityEngine;

public class SpoonController : MonoBehaviour
{
    [Header("Salt Objects On Spoon")]
    public GameObject calciumSalt;
    public GameObject bariumSalt;
    public GameObject potassiumSalt;

    public void SetCalcium()
    {
        calciumSalt.SetActive(true);
        bariumSalt.SetActive(false);
        potassiumSalt.SetActive(false);
    }

    public void SetBarium()
    {
        calciumSalt.SetActive(false);
        bariumSalt.SetActive(true);
        potassiumSalt.SetActive(false);
    }

    public void SetPotassium()
    {
        calciumSalt.SetActive(false);
        bariumSalt.SetActive(false);
        potassiumSalt.SetActive(true);
    }

    public void ClearSalt()
    {
        calciumSalt.SetActive(false);
        bariumSalt.SetActive(false);
        potassiumSalt.SetActive(false);
    }
}