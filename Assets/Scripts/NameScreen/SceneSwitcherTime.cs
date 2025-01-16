using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneSwitcherTime : MonoBehaviour
{
    public CanvasGroup flashCanvas; // CanvasGroup ��� �������
    public string sceneName;  // ��� ����� ��� ��������
    public float delay = 5f;  // �������� ����� ��������� (� ��������)
    public float fadeDurationdelay = 1f;
    public float WaitForSecond = 0.1f;
    private void Start()
    {
        // ������������� ��������� ������������
        flashCanvas.alpha = 0;

        // ��������� ������ ��� �������� � ����� �����
        StartCoroutine(FlashAndSwitchScene(sceneName));
    }

    private IEnumerator FlashAndSwitchScene(string sceneName)
    {
        // �������� �������� ����� ��������
        yield return new WaitForSeconds(delay);

        float time = 0;

        while (time < fadeDurationdelay)
        {
            time += Time.deltaTime;
            flashCanvas.alpha = Mathf.Lerp(0, 1, time / fadeDurationdelay);
            yield return null;
        }

        // ���� 1: ������ ��������� �������
        flashCanvas.alpha = 1;

        // ���� 2: ������� �� ����� �����
        yield return new WaitForSeconds(WaitForSecond); // ��������� ����� ��� �������
        FlashFadeOut.hasRun = false;
        if (sceneName != null) 
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
