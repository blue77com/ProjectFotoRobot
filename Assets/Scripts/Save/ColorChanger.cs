using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> textComponents; // Список объектов TextMeshProUGUI
    [SerializeField, Range(0f, 1f)] private float markedAlpha = 1f;    // Прозрачность для пометки
    [SerializeField, Range(0f, 1f)] private float unmarkedAlpha = 0.5f; // Прозрачность для разметки

    void Start()
    {
        // Применяем состояние из общего HashSet при старте
        LoadState();
    }

    public void MarkObjectByIndex(int index)
    {
        if (IsValidIndex(index))
        {
            ChangeAlphaForSingle(index, markedAlpha);
            SharedState.VisitedIndices.Add(index); // Добавляем индекс в общий HashSet
        }
    }

    public void UnmarkObjectByIndex(int index)
    {
        if (IsValidIndex(index))
        {
            ChangeAlphaForSingle(index, unmarkedAlpha);
            SharedState.VisitedIndices.Remove(index); // Удаляем индекс из общего HashSet
        }
    }

    private void ChangeAlphaForSingle(int index, float alpha)
    {
        if (IsValidIndex(index))
        {
            var color = textComponents[index].color; // Получаем текущий цвет
            color.a = alpha; // Изменяем альфа-канал
            textComponents[index].color = color; // Применяем изменённый цвет
        }
        else
        {
            Debug.LogWarning("Индекс вне диапазона или объект не задан!");
        }
    }

    private bool IsValidIndex(int index)
    {
        return index >= 0 && index < textComponents.Count && textComponents[index] != null;
    }

    private void LoadState()
    {
        foreach (var index in SharedState.VisitedIndices)
        {
            if (IsValidIndex(index))
            {
                ChangeAlphaForSingle(index, markedAlpha);
            }
        }
    }
}
