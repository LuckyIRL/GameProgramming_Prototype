using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneManager : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        // Load the specified scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
        Debug.Log("Game is quitting");
    }

    public void RestartGame()
    {
        // Restart the current scene
        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneName);
    }

    public void LoadMainMenu()
    {
        // Load the main menu scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void LoadGame()
    {
        // Load the game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    public void LoadNextLevel()
    {
        // Load the next level scene
        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetSceneByName(currentSceneName).buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex < UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more levels to load.");
        }
    }

    public void LoadScene(string sceneName, Dictionary<string, object> parameters)
    {
        // Load the specified scene with parameters
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        // Handle parameters if needed
        foreach (var param in parameters)
        {
            Debug.Log($"Parameter: {param.Key} = {param.Value}");
        }
    }
}
