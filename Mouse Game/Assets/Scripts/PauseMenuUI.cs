using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject pauseOverlay;
    private bool isPaused = false;

    private void Start()
    {
        if (pauseOverlay != null)
        {
            pauseOverlay.SetActive(false);
        } 
            Time.timeScale = 1f; // Ensure the game starts unpaused
        isPaused = false;
    }

    private void Update()
    {
        // Open / close pause menu with ESC key
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            TogglePause();
        }
    }

    public void TogglePause()
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

    public void PauseGame()
    {   
        if (pauseOverlay != null)
        {
            pauseOverlay.SetActive(true);
        } 
        Time.timeScale = 0f; // Pause the game
        isPaused = true;
    }

    public void ResumeGame()
    {
        if (pauseOverlay != null)
        {
            pauseOverlay.SetActive(false);
        }
        Time.timeScale = 1f; // Resume the game
        isPaused = false;
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; // Ensure time scale is reset before loading main menu
        isPaused = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
        {
            Debug.Log("Quitting game...");
            Application.Quit();
    }
}
