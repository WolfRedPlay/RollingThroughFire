using UnityEngine;

public class ElectroManager : MonoBehaviour
{

    private GameObject electro;

    void Start()
    {
        electro = GameObject.FindWithTag("Electro");
    }

    public void ElectroOff()
    {
        electro.SetActive(false);
    }
}
