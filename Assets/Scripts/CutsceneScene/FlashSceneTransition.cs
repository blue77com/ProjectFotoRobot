using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FlashSceneTransition : MonoBehaviour
{
    public CanvasGroup flashCanvas; // CanvasGroup для вспышки
    public float fadeDuration = 1f; // Длительность затухания в новой сцене

    // Метод для начала перехода
    public void TriggerFlashAndSwitchScene(string sceneName)
    {
        StartCoroutine(FlashAndSwitchScene(sceneName));
    }

    private IEnumerator FlashAndSwitchScene(string sceneName)
    {
        if (flashCanvas != null)
        {
            // Этап 1: Резкое появление вспышки
            flashCanvas.alpha = 0;

            // Этап 1: Плавное появление вспышки
            float elapsedTime = 0f;
            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                flashCanvas.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
                yield return null; // Ждем следующий кадр
            }
            // Убедимся, что вспышка полностью видна
            flashCanvas.alpha = 1;
        } 

        // Этап 2: Переход на новую сцену
        SceneManager.LoadScene(sceneName);

        // Этап 3: Ожидание завершения загрузки сцены
        yield return null;
    }
}
