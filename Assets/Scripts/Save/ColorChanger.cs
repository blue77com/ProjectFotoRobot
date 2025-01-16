using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> textComponents; // ������ �������� TextMeshProUGUI
    [SerializeField, Range(0f, 1f)] private float markedAlpha = 1f;    // ������������ ��� �������
    [SerializeField, Range(0f, 1f)] private float unmarkedAlpha = 0.5f; // ������������ ��� ��������

    void Start()
    {
        // ��������� ��������� �� ������ HashSet ��� ������
        LoadState();
    }

    public void MarkObjectByIndex(int index)
    {
        if (IsValidIndex(index))
        {
            ChangeAlphaForSingle(index, markedAlpha);
            SharedState.VisitedIndices.Add(index); // ��������� ������ � ����� HashSet
        }
    }

    public void UnmarkObjectByIndex(int index)
    {
        if (IsValidIndex(index))
        {
            ChangeAlphaForSingle(index, unmarkedAlpha);
            SharedState.VisitedIndices.Remove(index); // ������� ������ �� ������ HashSet
        }
    }

    private void ChangeAlphaForSingle(int index, float alpha)
    {
        if (IsValidIndex(index))
        {
            var color = textComponents[index].color; // �������� ������� ����
            color.a = alpha; // �������� �����-�����
            textComponents[index].color = color; // ��������� ��������� ����
        }
        else
        {
            Debug.LogWarning("������ ��� ��������� ��� ������ �� �����!");
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
