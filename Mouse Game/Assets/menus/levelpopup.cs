using UnityEngine;

public class levelpopup : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject uiLevel;

    void Start()
    {
        if (uiLevel != null)
            uiLevel.SetActive(false);
        }

private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player") && uiLevel != null)
        uiLevel.SetActive(true);
}

private void OnTriggerExit(Collider other)
{
    if (other.CompareTag("Player") && uiLevel != null)
        uiLevel.SetActive(false);
}
}
