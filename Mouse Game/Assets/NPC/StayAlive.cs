using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public CanvasGroup fadeGroup;
    public float fadeSpeed = 1.5f;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void StartFadeAndLoad(string sceneName)
    {
        StartCoroutine(FadeSequence(sceneName));
    }

    IEnumerator FadeSequence(string sceneName)
    {
        // Fade to Black
        while (fadeGroup.alpha < 1)
        {
            fadeGroup.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }

        // Load the Scene
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        while (!op.isDone) yield return null;

        // Fade to Clear
        while (fadeGroup.alpha > 0)
        {
            fadeGroup.alpha -= Time.deltaTime * fadeSpeed;
            yield return null;
        }

        Destroy(gameObject);
    }
}