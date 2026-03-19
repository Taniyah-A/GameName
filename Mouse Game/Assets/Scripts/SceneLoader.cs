using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("Scene to Load")]
    [SerializeField] private string targetSceneName;

    [Header("Settings")]
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private bool showDebugGizmo = true;
    [SerializeField] private Color gizmoColor = new Color(0f, 1f, 0.5f, 0.3f);


    private void OnTriggerEnter(Collider other){
        if (other.CompareTag(playerTag)){
            LoadTargetScene();
        }    
    }
    private void LoadTargetScene()
    {
        if (string.IsNullOrEmpty(targetSceneName))
        {
            Debug.LogError("Scene name is not set in the SceneLoader.");
            return;
        }
        SceneManager.LoadScene(targetSceneName);
    }

    private void OnDrawGizmos()
    {
        if (showDebugGizmo)
        {
            Gizmos.color = gizmoColor;
            Collider collider = GetComponent<Collider>();
            if (collider != null)
            {
                Gizmos.DrawCube(collider.bounds.center, collider.bounds.size);
            }
        }
    }
}
