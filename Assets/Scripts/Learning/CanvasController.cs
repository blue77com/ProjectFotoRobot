using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public Canvas canvas; // ������ �� Canvas
    private static int activationCount = 0; // ������� ��� ������������ �������

    // ������� ��� ��������� Canvas
    public void ShowCanvas()
    {
        if (activationCount < 2) // ��������, �������� �� ��� ������
        {
            if (canvas != null)
            {
                canvas.gameObject.SetActive(true); // �������� Canvas
                Time.timeScale = 0f; // ������ ���� �� �����
            }
        }
    }

    // ������� ��� ����������� Canvas
    public void HideCanvas()
    {
        if (activationCount < 2) // ��������, �������� �� ��� ������
        {
            if (canvas != null)
            {
                canvas.gameObject.SetActive(false); // ��������� Canvas
                Time.timeScale = 1f; // ���������� ���������� �������� ����
                activationCount++; // ����������� �������
            }
        }
    }
}
