using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEndManager : MonoBehaviour
{
    public GameObject uiBlocker; // UI �������, ����������� �������������� (��������, ���������� Panel)
    public Button nextSceneButton; // ������ ��� �������� �� ��������� �����
    public string nextSceneName = "NextScene"; // ��� ��������� �����
    private bool isGameEnd = false; // ����, �����������, ��� ���� ���������
    public static int layerNumber;
    public ColorChanger colorChanger;
    private void Start()
    {
        // ��������, ��� UI ������ � ������ ��������� � ������
        if (uiBlocker != null)
            uiBlocker.SetActive(false);

        if (nextSceneButton != null)
        {
            nextSceneButton.gameObject.SetActive(false);
            nextSceneButton.onClick.AddListener(LoadNextScene);
        }
    }

    public void CheckAndEndGame()
    {
        // ��������� ���������� ���� �������� � PhotoCaptureManager
        var photoManager = FindObjectOfType<CommentDisplayManager>();
        if (photoManager != null && photoManager.IsAnimationComplete() && !isGameEnd)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        isGameEnd = true;

        // ��������� UI
        if (uiBlocker != null)
            uiBlocker.SetActive(true);

        // �������� ����� ���������� ������
        StartCoroutine(ShowButtonWithDelay());
    }

    private System.Collections.IEnumerator ShowButtonWithDelay()
    {
        // �������� ������� (����� ��������� �����, ���� �����)
        yield return new WaitForSeconds(0.5f);

        // ���������� ������ �������� �� ��������� �����
        if (nextSceneButton != null)
            nextSceneButton.gameObject.SetActive(true);

        colorChanger.MarkObjectByIndex(layerNumber);
    }

    private void Update()
    {
        if (isGameEnd)
        {
            // ���������, ������ �� ������� Backspace
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                LoadNextScene();
            }
        }
    }

    private void LoadNextScene()
    {
        // ��������� ��������� �����
        SceneManager.LoadScene(nextSceneName);

    }
}
