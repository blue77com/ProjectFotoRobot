using UnityEngine;
using System.Collections;

public class FlashFadeOut : MonoBehaviour
{
    public CanvasGroup flashCanvas; // CanvasGroup для вспышки
    public float fadeDuration = 1f; // Длительность затухания
    public CutsceneManager cutsceneManager;
    public CanvasController controller;

    public static bool hasRun = false; // Статическая переменная

    private void Start()
    {
        if (hasRun)
        {
            flashCanvas.alpha = 0; // Полное исчезновение вспышки
            // Если скрипт уже выполнялся, отключаем его
            Debug.Log("FlashFadeOut уже выполнялся в этой игре.");
            return;
        }

        hasRun = true; // Указываем, что скрипт выполнен
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

        flashCanvas.alpha = 0; // Полное исчезновение вспышки

        if (cutsceneManager != null)
        {
            cutsceneManager.StartCutscene();
        }
        else
        {
            Debug.LogWarning("CutsceneManager не назначен или отсутствует в сцене.");
        }

        if (controller != null)
        {
            controller.ShowCanvas();
        }
        else
        {
            Debug.LogWarning("CutsceneManager не назначен или отсутствует в сцене.");
        }
    }
}
