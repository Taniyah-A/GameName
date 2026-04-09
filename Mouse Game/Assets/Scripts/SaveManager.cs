using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    // This class is responsible for managing the player's progress and saving/loading data.
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Save the current scene name
    public void SaveCurrentScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("SavedSceneName", currentScene);
        PlayerPrefs.Save();

        Debug.Log("Current scene saved: " + currentScene);
    }

    // Load the saved scene
    public void LoadSavedScene()
    {
        if (PlayerPrefs.HasKey("SavedSceneName"))
        {
            string savedScene = PlayerPrefs.GetString("SavedSceneName");
            Debug.Log("Loaded saved scene: " + savedScene);
            SceneManager.LoadScene(savedScene);
        }
        else
        {
            Debug.LogWarning("No saved game found.");
        }
    }

    // Start a fresh game by clearing saved data and loading the first scene
    public void StartNewGame(string firstSceneName)
    {
        PlayerPrefs.DeleteKey("SavedSceneName");
        PlayerPrefs.Save();
        SceneManager.LoadScene(firstSceneName);
    }

    public bool HasSavedGame()
    {
        return PlayerPrefs.HasKey("SavedSceneName");
    }
}
