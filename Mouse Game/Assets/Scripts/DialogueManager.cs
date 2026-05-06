using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour, Dialogue.IPlayerActions
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

    private Queue<string> sentences = new Queue<string>();
    private bool dialogueActive = false;
    public System.Action OnDialogueEnd;

    private Dialogue controls;

    private bool justEnded = false;
    
    public bool JustEndedDialogue()
    {
        return justEnded;
    }

    public bool IsDialogueActive()
    {
        return dialogueActive;
    }

    void Awake()
    {
        controls = new Dialogue();
        controls.Player.SetCallbacks(this);
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

    public void StartDialogue(string[] dialogueLines)
    {
        dialoguePanel.SetActive(true);

        sentences.Clear();

        foreach (string line in dialogueLines)
        {
            sentences.Enqueue(line);
        }

        dialogueActive = true;
        DisplayNextSentence();
    }


    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        dialogueText.text = "";
        dialogueActive = false;
        dialoguePanel.SetActive(false);

        Debug.Log("Dialogue ended.");

        justEnded = true;
        Invoke(nameof(ResetJustEnded), 0.2f); // small delay to prevent immediate re-triggering of dialogue

        OnDialogueEnd?.Invoke();
    }

    void ResetJustEnded()
    {
               justEnded = false;
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed && dialogueActive)
        {
            Debug.Log("Pressed E for next sentence");
            DisplayNextSentence();
        }
    }
}