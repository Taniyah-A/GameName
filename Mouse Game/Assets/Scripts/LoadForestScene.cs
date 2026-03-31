using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadForestScene : MonoBehaviour
{
    public void PlayGame() { 
        SceneManager.LoadScene("ForestScene");
    }

    public void LoadTutorial()
    {
               SceneManager.LoadScene("tutoral");
    }

    //public void LoadMainMenu()
    //{       
    //            // Save the scene the characters are in?
    //            SceneManager.LoadScene("MainMenu");
               
    //}

    //public void LoadGame()
    //{
    //    // Get a list of the games the player has saved and the times
    //    // Go to the load game scene and display the list of saved games and times
    //     SceneManager.LoadScene("LoadGame");
    //    // When the player selects a saved game, load the scene and set the player's position and stats to the saved values
    //}
}
