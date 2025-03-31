using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public Canvas mainCanvas;
    public Button resumeButton;
    public Button quitButton;


    void Start()
    {
        pauseMenu.SetActive(false);
        resumeButton.onClick.AddListener(ResumeGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeSelf)
            {
                ResumeGame();
                
            }
            else
            {
                PauseGame();
                
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        mainCanvas.enabled = false;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        mainCanvas.enabled = true;
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
