using UnityEngine;
using TMPro;
using System.Collections;

public class TextFadeIn : MonoBehaviour
{
    public TextMeshProUGUI text;  // Ссылка на компонент TextMeshProUGUI
    public float fadeDuration = 2f; // Длительность анимации
    public float delayBeforeFade = 1f; // Задержка перед началом анимации

    private void Start()
    {
        if (text == null)
            text = GetComponent<TextMeshProUGUI>();

        StartCoroutine(FadeInText());
    }

    private IEnumerator FadeInText()
    {
        text.alpha = 0; // Устанавливаем начальную прозрачность текста

        // Задержка перед началом анимации
        yield return new WaitForSeconds(delayBeforeFade);

        float elapsedTime = 0;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            text.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null; // Ждем следующего кадра
        }

        text.alpha = 1; // Устанавливаем полную видимость
    }
}
