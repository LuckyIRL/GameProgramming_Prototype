using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public Canvas mainCanvas;
    public Button resumeButton;
    public Button quitButton;
    private bool isPaused = false;
    private RobotInputActions inputActions;

    void Awake()
    {
        inputActions = new RobotInputActions();
    }

    void Start()
    {
        pauseMenu.SetActive(false);
        resumeButton.onClick.AddListener(ResumeGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    void OnEnable()
    {
        inputActions.UI_Interactions.Enable();
    }

    void OnDisable()
    {
        inputActions.UI_Interactions.Disable();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
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
        isPaused = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        mainCanvas.enabled = false;
        inputActions.UI_Interactions.Enable();
        EnableCursor();
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        mainCanvas.enabled = true;
        inputActions.UI_Interactions.Disable();
        DisableCursor();
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    private void EnableCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void DisableCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}