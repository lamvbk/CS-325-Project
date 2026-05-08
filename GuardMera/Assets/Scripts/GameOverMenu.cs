using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void RestartGame()
    {
        Time.timeScale = 1f; // Reset time scale in case it was paused
        SceneManager.LoadScene(1);
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f; // reset time scale in case it was paused
        SceneManager.LoadScene(0); // main menu at index 0 i belive
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}