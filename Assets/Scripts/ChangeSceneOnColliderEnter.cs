using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnColliderEnter : MonoBehaviour
{

    [SerializeField] SceneTransition mSceneTransition;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mSceneTransition.FadeIn();
            SceneManager.LoadScene("EndingScene");
        }
    }
}
