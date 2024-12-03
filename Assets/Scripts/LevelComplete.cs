using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    [SerializeField] private int itemCount; // ���������� ���������
    [SerializeField] private GameObject completionUI; // UI ������� ��� ����������
    [SerializeField] private string nextSceneName; // ��� ��������� �����

    private bool allItemsMarked = false;

    void Start()
    {
        // ���������� UI ��������
        if (completionUI != null)
        {
            completionUI.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Completion UI �� ��������!");
        }
    }

    void Update()
    {
        // ��������� ��������� ������ ���� ��� ��� �� ���� ���������
        if (!allItemsMarked && AllItemsMarked())
        {
            ShowCompletionUI();
        }
    }

    private bool AllItemsMarked()
    {
        // ���������, ���� �� ��� ������� �� 0 �� itemCount-1 � SharedState.VisitedIndices
        for (int i = 0; i < itemCount; i++)
        {
            if (!SharedState.VisitedIndices.Contains(i)) return false; // ���� ���� �� ���� ������ �����������, ���������� false
        }

        return true; // ��� ������� ������������
    }

    private void ShowCompletionUI()
    {
        allItemsMarked = true; // ��������� ���������
        if (completionUI != null)
        {
            completionUI.SetActive(true); // ���������� UI �������
        }
    }

    // ����� ��� �������� �� ��������� �����
    public void TransitionToNextScene()
    {
        Debug.Log("������� �� ��������� �����...");
        SceneManager.LoadScene(nextSceneName); // ������� �� ��������� �����
    }

    // ����� ��� ������ ���������
    public void ResetLevelState()
    {
        SharedState.VisitedIndices.Clear(); // ������� HashSet
        Debug.Log("��������� ������ ��������.");
    }
}
