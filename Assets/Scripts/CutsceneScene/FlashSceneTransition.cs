using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FlashSceneTransition : MonoBehaviour
{
    public CanvasGroup flashCanvas; // CanvasGroup ��� �������
    public float fadeDuration = 1f; // ������������ ��������� � ����� �����

    // ����� ��� ������ ��������
    public void TriggerFlashAndSwitchScene(string sceneName)
    {
        StartCoroutine(FlashAndSwitchScene(sceneName));
    }

    private IEnumerator FlashAndSwitchScene(string sceneName)
    {
        if (flashCanvas != null)
        {
            // ���� 1: ������ ��������� �������
            flashCanvas.alpha = 0;

            // ���� 1: ������� ��������� �������
            float elapsedTime = 0f;
            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                flashCanvas.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
                yield return null; // ���� ��������� ����
            }
            // ��������, ��� ������� ��������� �����
            flashCanvas.alpha = 1;
        } 

        // ���� 2: ������� �� ����� �����
        SceneManager.LoadScene(sceneName);

        // ���� 3: �������� ���������� �������� �����
        yield return null;
    }
}
