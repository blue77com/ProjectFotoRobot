using UnityEngine;
using TMPro;

public class CommentDisplayManager : MonoBehaviour
{
    public TextMeshProUGUI comment1; // Ссылка на первый текстовый компонент
    public TextMeshProUGUI comment2; // Ссылка на второй текстовый компонент
    public string[] commentsArray1; // Массив комментариев для первого текста
    public string[] commentsArray2; // Массив комментариев для второго текста
    public float delayBeforeShowingComments = 0.5f; // Задержка перед началом анимации
    public float fadeDuration = 1.0f; // Длительность анимации появления
    private bool isAnimationComplete = false; // Флаг завершения всех анимаций

    public void ShowComments(int index)
    {
        // Проверяем, чтобы индекс был в пределах массива
        if (index < 0 || index >= commentsArray1.Length || index >= commentsArray2.Length)
        {
            Debug.LogWarning("Индекс выходит за пределы массива.");
            return;
        }

        StartCoroutine(DisplayCommentsWithFade(index));
    }

    private System.Collections.IEnumerator DisplayCommentsWithFade(int index)
    {
        // Ждем перед началом анимации
        yield return new WaitForSeconds(delayBeforeShowingComments);

        // Получаем комментарии по индексу
        string selectedComment1 = commentsArray1[index];
        string selectedComment2 = commentsArray2[index];

        // Устанавливаем текст в компоненты TextMeshPro
        if (comment1 != null)
        {
            comment1.text = selectedComment1;
            yield return StartCoroutine(FadeInText(comment1)); // Анимация появления первого текста
        }

        if (comment2 != null)
        {
            comment2.text = selectedComment2;
            yield return StartCoroutine(FadeInText(comment2)); // Анимация появления второго текста
        }

        isAnimationComplete = true;

        FindObjectOfType<GameEndManager>()?.CheckAndEndGame();
    }

    private System.Collections.IEnumerator FadeInText(TextMeshProUGUI text)
    {
        float elapsedTime = 0;
        Color originalColor = text.color;

        // Устанавливаем начальную прозрачность на 0
        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);

        while (elapsedTime < fadeDuration)
        {
            // Постепенно увеличиваем альфа-канал
            float alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Устанавливаем альфа-канал на 1 (полностью видимый)
        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1);
    }

    public bool IsAnimationComplete()
    {
        return isAnimationComplete;
    }
}
