using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnColliderEnter : MonoBehaviour
{

    private SceneTransition mSceneTransition;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mSceneTransition.FadeIn();
            SceneManager.LoadScene("EndingScene");
        }
    }
}
