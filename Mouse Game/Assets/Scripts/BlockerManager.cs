using UnityEngine;

public class BlockerManager : MonoBehaviour
{
    [System.Serializable]
    public class Blocker
    {
        public GameObject blockerObject;
        public int requiredCompletedLevel;
    }

    [Header("Blockers")]
    [SerializeField] private Blocker[] blockers;

    private void Start()
    {
        UpdateBlockers();
    }

    private void UpdateBlockers()
    {
        int lastCompletedLevel = PlayerPrefs.GetInt("LastCompletedLevel", 0);

        foreach (Blocker blocker in blockers)
        {
            if (blocker.blockerObject == null)
            {
                Debug.LogWarning("BlockerManager: A blocker entry has no GameObject assigned.");
                continue;
            }

            bool shouldBeRemoved = lastCompletedLevel >= blocker.requiredCompletedLevel;
            Debug.Log($"{blocker.blockerObject.name} | required: {blocker.requiredCompletedLevel} | removed: {shouldBeRemoved}");
            blocker.blockerObject.SetActive(!shouldBeRemoved);
            
        }
    }
}
