using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLeave : MonoBehaviour
{
    private bool isLoading = false;

    // Optional: Reference to your Fade Group to ensure it's "On" before loading
    public CanvasGroup fadeGroup;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isLoading)
        {
            StartCoroutine(LoadSceneAsync());
        }
    }

    IEnumerator LoadSceneAsync()
    {
        isLoading = true;


        if (fadeGroup != null) fadeGroup.alpha = 1f;


        for (int i = 0; i < 5; i++)
        {
            yield return null;
        }


        AsyncOperation operation = SceneManager.LoadSceneAsync("ForestScene");


        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);


            yield return null;
        }
    }
}