using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class DialogueUIController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text characterName;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Image characterImage;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Transform choicesContainer;
    [SerializeField] private ChoiceButton choiceButtonPrefab;

    [Header("Settings")]
    [SerializeField] private float charsPerSecond = 30f;

    private Coroutine _typingRoutine;
    private DialogueNode _currentNode;
    private readonly List<ChoiceButton> _activeChoiceButtons = new();

    // Пул объектов для кнопок
    private ObjectPool<ChoiceButton> _buttonPool;

    private void Awake()
    {
        // Инициализация пула кнопок
        _buttonPool = new ObjectPool<ChoiceButton>(
            createFunc: () => Instantiate(choiceButtonPrefab, choicesContainer),
            actionOnGet: button => button.gameObject.SetActive(true),
            actionOnRelease: button => {
                button.gameObject.SetActive(false);
                button.OnClickAction.RemoveAllListeners();
            },
            actionOnDestroy: button => Destroy(button.gameObject)
        );
    }

    private void OnEnable() => DialogueEvents.NodeChanged += OnNodeChanged;
    private void OnDisable() => DialogueEvents.NodeChanged -= OnNodeChanged;

    private void OnNodeChanged(DialogueNode node)
    {
        _currentNode = node;
        ClearChoices();
        UpdateCharacterUI();
        UpdateBackground();
        StartDialogueText(node.Text);
    }

    private void UpdateCharacterUI()
    {
        characterName.text = _currentNode.Character;
        characterImage.sprite = CharacterDatabase.GetSprite(
            _currentNode.Character,
            _currentNode.Emotion
        );
    }

    private void UpdateBackground() =>
        backgroundImage.sprite = BackgroundDatabase.GetSprite(_currentNode.BackgroundID);

    private void StartDialogueText(string text)
    {
        if (_typingRoutine != null) StopCoroutine(_typingRoutine);
        _typingRoutine = StartCoroutine(TypeText(text));
    }

    private IEnumerator TypeText(string text)
    {
        dialogueText.text = "";
        foreach (char c in text)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(1f / charsPerSecond);
        }
        CreateChoiceButtons();
        _typingRoutine = null;
    }

    public void SkipTyping()
    {
        if (_typingRoutine == null) return;

        StopCoroutine(_typingRoutine);
        dialogueText.text = _currentNode.Text;
        CreateChoiceButtons();
        _typingRoutine = null;
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (_typingRoutine != null)
            {
                SkipTyping();
            }
            else if(_currentNode.Choices.Count == 0 && !string.IsNullOrEmpty(_currentNode.NextNodeID))
            {
                DialogueSystem.Instance.LoadNextNode();
            }
        }
    }

    private void CreateChoiceButtons()
    {
        if (_currentNode.Choices.Count == 0) return;

        foreach (var choice in _currentNode.Choices)
        {
            _activeChoiceButtons.Add(ChoiceFactory.CreateChoiceButton(
                choice: choice,
                container: choicesContainer,
                buttonPool: _buttonPool,
                onClickAction: () => DialogueSystem.Instance.SelectChoice(choice)
            ));
        }
    }

    private void ClearChoices()
    {
        foreach (var button in _activeChoiceButtons)
        {
            _buttonPool.Release(button);
        }
        _activeChoiceButtons.Clear();
    }
}