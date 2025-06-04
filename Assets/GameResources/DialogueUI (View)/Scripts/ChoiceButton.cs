using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ChoiceButton : MonoBehaviour
{
    public UnityEvent OnClickAction;

    [SerializeField]
    protected TMP_Text choiceText;

    protected DialogueChoice choice;
    protected Button button;

    protected virtual void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public virtual void Init(DialogueChoice dialogueChoice)
    {
        choice = dialogueChoice;
        choiceText.text = choice.Text;
    }

    protected virtual void OnDestroy()
    {
        button.onClick.RemoveListener(OnClick);
    }

    protected virtual void OnClick()
    {
        OnClickAction.Invoke();
    }
}
