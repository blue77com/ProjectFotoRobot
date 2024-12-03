using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private List<Image> images; // ������ �������� Image
    [SerializeField] private Color markedColor = Color.red;   // ���� ��� �������
    [SerializeField] private Color unmarkedColor = Color.black; // ���� ��� ��������

    void Start()
    {
        // ��������� ��������� �� ������ HashSet ��� ������
        LoadState();
    }

    public void MarkObjectByIndex(int index)
    {
        if (IsValidIndex(index))
        {
            ChangeColorForSingle(index, markedColor);
            SharedState.VisitedIndices.Add(index); // ��������� ������ � ����� HashSet
        }
    }

    public void UnmarkObjectByIndex(int index)
    {
        if (IsValidIndex(index))
        {
            ChangeColorForSingle(index, unmarkedColor);
            SharedState.VisitedIndices.Remove(index); // ������� ������ �� ������ HashSet
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
            Debug.LogWarning("������ ��� ��������� ��� ������ �� �����!");
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
