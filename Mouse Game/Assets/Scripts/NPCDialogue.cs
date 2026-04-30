using UnityEngine;

[CreateAssetMenu(menuName = "NPC Dialogue")]
public class NPCDialogue : ScriptableObject
{
    public string npcID;
    public string npcName;

    [TextArea(2, 5)]
    public string[] FirstTimeDialogue;

    [TextArea(2, 5)]
    public string[] ReturnedDialogue;

    [TextArea(2, 5)]
    public string[] ThirdDialogue;

    [TextArea(2, 5)]
    public string[] LastLevelDialogue;

    [TextArea(2, 5)]
    public string[] AllLevelsCompleted;
}