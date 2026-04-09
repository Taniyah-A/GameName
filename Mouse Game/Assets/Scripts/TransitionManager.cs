using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour
{
    
    public static TransitionManager Instance { get; private set; }


    [Header("Fade Settings")]
    [SerializeField] private Image fadePanel;
    [SerializeField] private float fadeDuration = 1f;

    [Header("Particles")]
    [SerializeField] private ParticleSystem transitionParticles;
    
    
    
    private void Awake()
    {
        //Ensure only one instance of TransitionManager exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SetFadeAlpha(0f);
        
        
    }

    public void TransitionToScene(string sceneName)
    {
        StartCoroutine(TransitionRoutine(sceneName));
    }

    private IEnumerator TransitionRoutine(string sceneName)
    {
        // Play transition particles
        if (transitionParticles != null)
        {
            transitionParticles.Play();
        }

        // Fade out
        yield return StartCoroutine(Fade(0f, 1f));

        // Load the new scene
        SceneManager.LoadScene(sceneName);

        // Fade in
        yield return null; // Wait a frame to ensure the scene has loaded
        yield return StartCoroutine(Fade(1f, 0f));
    }

    private IEnumerator Fade(float from, float to){
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime; // Increment elapsed time
            SetFadeAlpha(Mathf.Lerp(from, to, elapsed / fadeDuration)); // Lerp alpha value based on elapsed time
            yield return null; // Wait for the next frame
        }
        SetFadeAlpha(to); // Ensure final alpha is set
    }

    private void SetFadeAlpha(float alpha)
    {
        if (fadePanel == null) return;
        Color c = fadePanel.color;
        c.a = alpha;
        fadePanel.color = c;
    
    }
    
}
