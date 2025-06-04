using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueData")]
public class DialogueData : ScriptableObject
{
    public string StartNodeID;
    public List<DialogueNode> Nodes = new();

    public DialogueNode GetNode(string id) =>
        Nodes.FirstOrDefault(n => n.ID == id);
}
