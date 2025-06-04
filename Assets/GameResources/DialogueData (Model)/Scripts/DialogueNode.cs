using System;
using System.Collections.Generic;

[Serializable]
public class DialogueNode
{
    public string ID;
    public string Character;
    public string Emotion = "neutral";
    public string Text;
    public string BackgroundID;
    public List<DialogueChoice> Choices = new List<DialogueChoice>();
    public string NextNodeID;
}
