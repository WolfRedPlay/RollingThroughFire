using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PhoneLogicUI : MonoBehaviour
{
    public GameObject homeScreen;
    public GameObject messagesScreen;
    public GameObject settingsScreen;
    public GameObject emergencyScreen;

    [SerializeField] AudioSource audioSource;


    private void Start()
    {
        var buttons = FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (var button in buttons)
        {
            button.onClick.AddListener(PlaySound);
        }
    }

    public void PlaySound()
    {
        audioSource.Play();
    }

    private void CloseAllScreens()
    {
        //homeScreen.SetActive(false);
        settingsScreen.SetActive(false);
        messagesScreen.SetActive(false);
    }

    // Method to go to the Home screen
    public void GoToHome()
    {
        homeScreen.SetActive(true);
        CloseAllScreens();
    }

    // Method to open the Settings screen
    public void OpenSettings()
    {
        homeScreen.SetActive(false);
        settingsScreen.SetActive(true);
    }

    // Method to activate the Flashlight screen
    public void ActivateFlashlight()
    {
        CloseAllScreens();
        //flashlightScreen.SetActive(true);
    }

    // Method to open the Messages screen
    public void OpenMessages()
    {
        homeScreen.SetActive(false);
        messagesScreen.SetActive(true);
    }

    // Method to open the Save/Load screen
    public void LoadCheckpoint()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

