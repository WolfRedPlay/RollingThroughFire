using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElevatorButton : MonoBehaviour
{
    private SceneTransition mSceneTransition;
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
        mSceneTransition.FadeIn();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("BottomFloor");
    }
}