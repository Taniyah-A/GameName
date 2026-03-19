using UnityEngine;

public class MouseNPCTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;

    private bool hasSpoken = false;
    string[] dialogueLines = new string[]
    {
        "Have you seen Micerie?",
        "The little scoundrel has run off again.",
        "I can't find him anywhere.",
        "I'm sure he's around here somewhere, but I just can't find him.",
        "If you see him, please let me know. He's a good mouse"
    };

    void OnTriggerEnter(Collider other) 
    { 
        if (other.CompareTag("Player") && !hasSpoken) {
            dialogueManager.StartDialogue(dialogueLines);
            hasSpoken = true;
        }
    }   
}
