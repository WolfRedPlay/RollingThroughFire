using UnityEngine;
using System.Threading;
using System.Collections;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private GameObject fireFix;
    public GameObject raycastObj;
    public GameObject targetObj;
    private bool mhasHit = false;
    private bool mhasHit1 = false;
    private bool mhasHit2 = false;
    private bool mhasHit3 = false;

    private void Awake()
    {
        fireFix = GameObject.FindWithTag("FireFix");
    }

    public void StartFire()
    {
        RaycastHit objectHit;
        Vector3 left = raycastObj.transform.TransformDirection(Vector3.up);
        Vector3 right = raycastObj.transform.TransformDirection(Vector3.down);
        Vector3 up = raycastObj.transform.TransformDirection(Vector3.forward);
        Vector3 down = raycastObj.transform.TransformDirection(Vector3.back);
        Debug.DrawRay(raycastObj.transform.position, left * 50, Color.red);
        Debug.DrawRay(raycastObj.transform.position, right * 50, Color.red);
        Debug.DrawRay(raycastObj.transform.position, up * 50, Color.red);
        Debug.DrawRay(raycastObj.transform.position, down * 50, Color.red);
        if (Physics.Raycast(raycastObj.transform.position, right, out objectHit, 50) && !mhasHit)
        {
            Vector3 incomingVec = objectHit.point - raycastObj.transform.position;
            Vector3 reflectVec = Vector3.Reflect(incomingVec, objectHit.normal);
            Debug.DrawRay(objectHit.point, reflectVec, Color.red);
            Instantiate(targetObj, objectHit.point, Quaternion.identity);
            mhasHit = true;
        }
        if (Physics.Raycast(raycastObj.transform.position, up, out objectHit, 50) && !mhasHit1)
        {
            Vector3 incomingVec = objectHit.point - raycastObj.transform.position;
            Vector3 reflectVec = Vector3.Reflect(incomingVec, objectHit.normal);
            Debug.DrawRay(objectHit.point, reflectVec, Color.red);
            Instantiate(targetObj, objectHit.point, Quaternion.identity);
            mhasHit1 = true;
        }
        if (Physics.Raycast(raycastObj.transform.position, down, out objectHit, 50) && !mhasHit2)
        {
            Vector3 incomingVec = objectHit.point - raycastObj.transform.position;
            Vector3 reflectVec = Vector3.Reflect(incomingVec, objectHit.normal);
            Debug.DrawRay(objectHit.point, reflectVec, Color.red);
            Instantiate(targetObj, objectHit.point, Quaternion.identity);
            mhasHit2 = true;
        }
    }

    private IEnumerator FixFire()
    {
        yield return new WaitForSeconds(5);
        fireFix.SetActive(true);
    }

}
