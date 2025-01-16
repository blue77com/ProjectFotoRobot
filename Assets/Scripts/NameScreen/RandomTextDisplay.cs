using TMPro;
using UnityEngine;

public class RandomTextDisplay : MonoBehaviour
{
    public TMP_Text textMeshPro;  // ������ �� ��������� TextMeshPro
    public string[] textList;     // ������ �������, �� ������� ����� �������� ���������

    // ���� ����� ����� ���������� ��� ����������� ���������� ������
    private void Start()
    {
        DisplayRandomText();
    }
    public void DisplayRandomText()
    {
        if (textList.Length > 0)
        {
            // �������� ��������� ������ �� ������
            int randomIndex = Random.Range(0, textList.Length);

            // ������ ����� � TextMesh Pro �� ��������� �� ������
            textMeshPro.text = textList[randomIndex];
        }
    }
}
