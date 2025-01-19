using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartNewGame()
    {
        FindAnyObjectByType<DataPersistenceManager>(FindObjectsInactive.Include).NewGame();
        SceneManager.LoadScene(1);
    }
    
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
}
