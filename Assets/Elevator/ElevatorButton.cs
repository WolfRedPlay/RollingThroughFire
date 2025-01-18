using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElevatorButton : MonoBehaviour
{
    private ElevatorManager mElevatorManager;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hand"))
        {
            this.transform.position -= new Vector3(-0.6f, 0, 0);
            StartCoroutine(LoadScene());
        }
    }

    private IEnumerator LoadScene()
    {
        mElevatorManager.PlayAnimation();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("TopFloor");
    }
}