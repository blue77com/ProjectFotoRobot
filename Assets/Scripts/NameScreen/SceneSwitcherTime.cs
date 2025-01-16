using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneSwitcherTime : MonoBehaviour
{
    public CanvasGroup flashCanvas; // CanvasGroup для вспышки
    public string sceneName;  // Имя сцены для загрузки
    public float delay = 5f;  // Задержка перед переходом (в секундах)
    public float fadeDurationdelay = 1f;
    public float WaitForSecond = 0.1f;
    private void Start()
    {
        // Устанавливаем начальную прозрачность
        flashCanvas.alpha = 0;

        // Запускаем таймер для перехода в новую сцену
        StartCoroutine(FlashAndSwitchScene(sceneName));
    }

    private IEnumerator FlashAndSwitchScene(string sceneName)
    {
        // Ожидание задержки перед вспышкой
        yield return new WaitForSeconds(delay);

        float time = 0;

        while (time < fadeDurationdelay)
        {
            time += Time.deltaTime;
            flashCanvas.alpha = Mathf.Lerp(0, 1, time / fadeDurationdelay);
            yield return null;
        }

        // Этап 1: Резкое появление вспышки
        flashCanvas.alpha = 1;

        // Этап 2: Переход на новую сцену
        yield return new WaitForSeconds(WaitForSecond); // Небольшая пауза для эффекта
        FlashFadeOut.hasRun = false;
        if (sceneName != null) 
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
