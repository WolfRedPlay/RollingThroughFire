using System.Collections;
using TMPro;
using UnityEngine;

public class Test : MonoBehaviour
{
    public TMP_Text buttonText;

    public void OnClicked()
    {
        buttonText.text = "pressed";
        StartCoroutine(NotPressed());
    }

    IEnumerator NotPressed()
    {
        yield return new WaitForSeconds(1f);
        buttonText.text = "Button";

    }
}
