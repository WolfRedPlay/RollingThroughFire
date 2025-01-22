using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainMenu : MonoBehaviour
{
    public void OnButtonPressed()
    {
        SceneManager.LoadScene(0);
    }
}
