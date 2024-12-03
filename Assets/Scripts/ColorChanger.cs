using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private List<Image> images; // Список объектов Image
    [SerializeField] private Color markedColor = Color.red;   // Цвет для пометки
    [SerializeField] private Color unmarkedColor = Color.black; // Цвет для разметки

    void Start()
    {
        // Применяем состояние из общего HashSet при старте
        LoadState();
    }

    public void MarkObjectByIndex(int index)
    {
        if (IsValidIndex(index))
        {
            ChangeColorForSingle(index, markedColor);
            SharedState.VisitedIndices.Add(index); // Добавляем индекс в общий HashSet
        }
    }

    public void UnmarkObjectByIndex(int index)
    {
        if (IsValidIndex(index))
        {
            ChangeColorForSingle(index, unmarkedColor);
            SharedState.VisitedIndices.Remove(index); // Удаляем индекс из общего HashSet
        }
    }

    private void ChangeColorForSingle(int index, Color color)
    {
        if (IsValidIndex(index))
        {
            images[index].color = color;
        }
        else
        {
            Debug.LogWarning("Индекс вне диапазона или объект не задан!");
        }
    }

    private bool IsValidIndex(int index)
    {
        return index >= 0 && index < images.Count && images[index] != null;
    }

    private void LoadState()
    {
        foreach (var index in SharedState.VisitedIndices)
        {
            if (IsValidIndex(index))
            {
                images[index].color = markedColor;
            }
        }
    }
}
