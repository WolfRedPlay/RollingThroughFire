using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject _recenteringWindow;
    [SerializeField] GameObject _mainMenuWindow;

    private void Start()
    {
        _recenteringWindow.SetActive(true);
        _mainMenuWindow.SetActive(false);
    }
    public void StartNewGame()
    {
        FindAnyObjectByType<DataPersistenceManager>(FindObjectsInactive.Include).NewGame();
        SceneManager.LoadScene(1);
    }
    
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }


    public void CloseRecenteringWindow()
    {
        _recenteringWindow.SetActive(false);
        _mainMenuWindow.SetActive(true);

    }
}
