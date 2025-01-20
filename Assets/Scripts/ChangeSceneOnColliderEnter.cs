using UnityEngine;

public class ChangeSceneOnColliderEnter : MonoBehaviour
{

    private SceneTransition mSceneTransition;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

        }
    }
}
