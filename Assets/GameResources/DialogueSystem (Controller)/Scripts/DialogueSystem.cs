using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance;

    [SerializeField] protected DialogueData data;
    protected DialogueNode currentNode;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            CharacterDatabase.Initialize();
            BackgroundDatabase.Initialize();
        }
    }

    private void Start()
    {
        Initialize();
    }

    public virtual void Initialize() =>
        LoadNode(data.StartNodeID);

    public virtual void LoadNode(string nodeID)
    {
        currentNode = data.GetNode(nodeID);
        DialogueEvents.NodeChanged?.Invoke(currentNode);
    }

    public virtual void LoadNextNode()
    {
        LoadNode(currentNode.NextNodeID);
    }

    public virtual void SelectChoice(DialogueChoice choice)
    {
        LoadNode(choice.NextNodeID);
    }
}
