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
        if (electro != null) 
            electro.SetActive(false);
    }
}
