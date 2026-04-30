using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class MouseNPCTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public TextMeshProUGUI promptText;
    public NPCDialogue dialogueData;

    private bool startedDialogue = false;

    private bool playerInRange = false;

    bool HasTalkedToNPC()
    {
        int completed = PlayerPrefs.GetInt("LastCompletedLevel", 0);
        string key = dialogueData.npcID + "_talked_" + completed;
        return PlayerPrefs.GetInt(key, 0) == 1;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered NPC trigger area.");

            if (promptText == null)
            {
                Debug.LogError("Prompt Text is not assigned in the inspector!");
                return;
            }

            if (dialogueData == null)
            {
                Debug.LogError("Dialogue Data is not assigned in the inspector!");
                return;
            }
            playerInRange = true;
            // Show the prompt with NPC name
            if (!string.IsNullOrEmpty(dialogueData.npcName))
            {
                promptText.text = "Press E to talk to " + dialogueData.npcName;
            }
            else
            {
                promptText.text = "Press E to talk";
            }
            promptText.gameObject.SetActive(true);
        }
    }

    void Start()
    {
        if (dialogueManager != null)
        {
            dialogueManager.OnDialogueEnd += OnDialogueFinished;
        }
    }

    void OnDialogueFinished()
    {
        if (startedDialogue)
        {
            MarkerNPCAsTalked();
            startedDialogue = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            promptText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (!playerInRange) return;

        if (HasTalkedToNPC())
        {
            promptText.text = dialogueData.npcName + " has nothing left to say for now.";
            return;
        }

        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (dialogueManager.IsDialogueActive())
            {
                return;
            }
            Debug.Log("Starting dialgue with " + dialogueData.npcName);

            startedDialogue = true;
            dialogueManager.StartDialogue(GetDialogue());

            promptText.gameObject.SetActive(false);
        }
    }

    string[] GetDialogue()
    {

        if (dialogueData == null)
        {
            Debug.LogError("Dialogue Data is not assigned in the inspector!");
            return new string[] { "No dialogue available." };
        }

        int completed = PlayerPrefs.GetInt("LastCompletedLevel", 0);
        Debug.Log("Completed level: " + completed);

        if (completed <= 0)
        {
            return dialogueData.FirstTimeDialogue;
        }
        else if (completed == 1)
        {
            return dialogueData.ReturnedDialogue;
        }
        else if (completed == 2)
        {
            return dialogueData.ThirdDialogue;
        }
        else
        {
            return dialogueData.LastLevelDialogue;
        }
    }

    void MarkerNPCAsTalked()
    {
        int completed = PlayerPrefs.GetInt("LastCompletedLevel", 0);
        string key = dialogueData.npcID + "_talked_level_" + completed;

        PlayerPrefs.SetInt(key, 1);
        PlayerPrefs.Save();
        Debug.Log("Saved talk state: " + key);
    }
}