using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static string[] levels = { "Level1", "Level2", "Level3" };
    private static int levelCount = 0;

    public static void LoadNextScene()
    {
        levelCount++;
        SceneManager.LoadScene(levels[levelCount]);
    }
}
