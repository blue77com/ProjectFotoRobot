using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu; // ������ �� ����
    public Button firstButton;   // ������ "����������"
    private bool isPaused = false;

    // ������� �������, � ������� ���� ����� �������� (��������, ��� ����)
    public RectTransform menuArea; // ������������� ������� ��� ����

    void Start()
    {
        // ���������, ��� ���� ������ � ������
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        // ��������/�������� ���� �� ������� Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        // ��������, ��������� �� ���� ��� UI � ���������� �������
        if (isPaused)
        {
            if (IsMouseOverUI())
            {
                // ���� ���� ��� UI, ���������� ����� � UI � ������������� ���������� �����������
                EventSystem.current.SetSelectedGameObject(null);
            }
            else
            {
                // ���� ���� ����� �� ������� UI, ��������������� ����� �� ������ ������
                if (EventSystem.current.currentSelectedGameObject == null)
                {
                    // �������� �� ������� ������� ������� �����, ����� ������� ����� �� ������ ������
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
                    }
                }
            }
        }
    }

    // ��������, ��������� �� ���� � �������� UI � � �������� �������� �������
    private bool IsMouseOverUI()
    {
        // �������� ������� ���� � ��������
        Vector2 mousePosition = Input.mousePosition;

        // ����������� ���� � ������������ ������� ���������
        if (!IsMouseInsideBounds(mousePosition))
        {
            return false; // ���� �� ��������� ����������� �������
        }

        // ������� ������ PointerEventData, ������� �������� ���������� � ��������� ����
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
        {
            position = mousePosition
        };

        // ������� ������ ��� �������� ���� ��������� Raycast
        List<RaycastResult> raycastResults = new List<RaycastResult>();

        // ��������� Raycast � ���������� ��� ���������� � ������
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        // ���� ������ �� ������, ������ ���� ��������� � �������� UI ��������
        return raycastResults.Count > 0;
    }

    // ��������, ��������� �� ���� ������ �������� ������� (menuArea)
    private bool IsMouseInsideBounds(Vector2 mousePosition)
    {
        // ����������� ���� � ������������ ��������� RectTransform
        Vector2 localPoint = menuArea.InverseTransformPoint(mousePosition);

        // ���������, ���� ������� ���� ������ �����������
        return menuArea.rect.Contains(localPoint);
    }

    public void Resume()
    {
        // ������ ���� � ����������� ����
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void QuitGame()
    {
        // ����� �� ����
        Application.Quit();
    }

    void Pause()
    {
        // �������� ���� � ������������� ����
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        // ������������� ����� �� ������ ������ ��� �������� ����
        EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
    }
}
