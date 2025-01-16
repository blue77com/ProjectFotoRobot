using UnityEngine;
using System.Collections;

public class FlashFadeOut : MonoBehaviour
{
    public CanvasGroup flashCanvas; // CanvasGroup ��� �������
    public float fadeDuration = 1f; // ������������ ���������
    public CutsceneManager cutsceneManager;
    public CanvasController controller;

    public static bool hasRun = false; // ����������� ����������

    private void Start()
    {
        if (hasRun)
        {
            flashCanvas.alpha = 0; // ������ ������������ �������
            // ���� ������ ��� ����������, ��������� ���
            Debug.Log("FlashFadeOut ��� ���������� � ���� ����.");
            return;
        }

        hasRun = true; // ���������, ��� ������ ��������
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float time = 0;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            flashCanvas.alpha = Mathf.Lerp(1, 0, time / fadeDuration);
            yield return null;
        }

        flashCanvas.alpha = 0; // ������ ������������ �������

        if (cutsceneManager != null)
        {
            cutsceneManager.StartCutscene();
        }
        else
        {
            Debug.LogWarning("CutsceneManager �� �������� ��� ����������� � �����.");
        }

        if (controller != null)
        {
            controller.ShowCanvas();
        }
        else
        {
            Debug.LogWarning("CutsceneManager �� �������� ��� ����������� � �����.");
        }
    }
}
