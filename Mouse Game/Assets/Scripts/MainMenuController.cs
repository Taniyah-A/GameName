using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    [SerializeField] private string firstSceneName = "ForestScene";


   public void NewGame()
    {
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.StartNewGame(firstSceneName);
        }
        else
        {
            SceneManager.LoadScene(firstSceneName);
        }
    }


    public void LoadGame()
    {
        if(SaveManager.Instance != null)
        {
            SaveManager.Instance.LoadSavedScene();
        } else
        {
            Debug.LogWarning("SaveManager instance not found. Cannot load saved game.");
        }
    }
}
