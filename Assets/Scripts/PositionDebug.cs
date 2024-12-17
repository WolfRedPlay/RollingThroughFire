using TMPro;
using UnityEngine;

public class PositionDebug : MonoBehaviour
{
    public Transform originTransform;
    public Transform cameraTransform;


    public TMP_Text debugField1;
    public TMP_Text debugField2;
    public TMP_Text debugField3;
    public TMP_Text debugField4;



    private void Update()
    {
        debugField1.text = originTransform.position.ToString();
        debugField2.text = originTransform.rotation.ToString();
        debugField3.text = cameraTransform.position.ToString();
        debugField4.text = cameraTransform.rotation.ToString();
    }
}
