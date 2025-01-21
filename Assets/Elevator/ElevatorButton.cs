using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElevatorButton : MonoBehaviour
{
    [SerializeField] private SceneTransition mSceneTransition;
    public void LoadScene()
    {
        StartCoroutine(LoadSceneCor());
    }

    private IEnumerator  LoadSceneCor()
    {
        mSceneTransition.FadeIn();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("BottomFloor");
    }
}