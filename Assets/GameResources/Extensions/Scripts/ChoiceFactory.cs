using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

// Фабрика для создания кнопок выбора
public static class ChoiceFactory
{
    public static ChoiceButton CreateChoiceButton(
        DialogueChoice choice,
        Transform container,
        ObjectPool<ChoiceButton> buttonPool,
        UnityEngine.Events.UnityAction onClickAction)
    {
        ChoiceButton button = buttonPool.Get();
        button.transform.SetParent(container, false);
        button.Init(choice);
        button.OnClickAction.RemoveAllListeners();
        button.OnClickAction.AddListener(onClickAction);
        return button;
    }
}
