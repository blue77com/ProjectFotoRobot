using TMPro;
using UnityEngine;

public class TextChangeOnClick : MonoBehaviour
{
    public TMP_Text textMeshPro;  // ������ �� ��������� TextMeshPro
    public int requiredClicks = 5;  // ���������� �������, ����� ������� �������� �����
    private int clickCount = 0;     // ������� �������
    public string Text;

    // ���� ����� ����� ���������� ��� ������� �� ������
    public void OnButtonClick()
    {
        clickCount++;

        // ���������, ������ �� ������� ���������� ���������� �������
        if (clickCount >= requiredClicks)
        {
            // ������ �����
            textMeshPro.text = Text;
        }
    }
}
