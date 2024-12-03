using UnityEngine;
using UnityEngine.UI;

public class PhotoCaptureManager : MonoBehaviour
{
    public FlashEffect flashEffect; // Ссылка на скрипт вспышки
    public RectTransform uiElement; // Ссылка на UI элемент
    public Image backgroundDimmer; // Ссылка на Image затемняющего слоя
    public Image displayImage; // Ссылка на UI Image для смены изображения
    public Sprite[] imageList; // Список изображений
    public Vector2 startPosition; // Начальная позиция
    public Vector2 endPosition; // Конечная позиция
    public float animationDuration = 1.0f; // Длительность анимации
    public float delayBeforeAnimation = 0.5f; // Задержка перед началом анимации
    public Color dimColor = new Color(0, 0, 0, 0.5f); // Цвет затемнения (черный с альфа = 0.5)

    public bool hasSuccessfulPhoto = false; // Флаг успешного фото
    private bool isSliding = false; // Флаг для предотвращения повторного запуска анимации
    public static int index;

    private void Start()
    {
        // Устанавливаем начальную позицию элемента
        uiElement.anchoredPosition = startPosition;

        // Устанавливаем начальную прозрачность затемняющего слоя
        if (backgroundDimmer != null)
        {
            backgroundDimmer.color = new Color(dimColor.r, dimColor.g, dimColor.b, 0); // Прозрачный
        }

        // Устанавливаем начальное изображение
        if (displayImage != null && imageList.Length > 0)
        {
            displayImage.sprite = imageList[Mathf.Clamp(index, 0, imageList.Length - 1)];
        }
    }

    public void UpdateAfterFlash(bool IsWin)
    {
        if (IsWin && !isSliding && !hasSuccessfulPhoto)
        {
            hasSuccessfulPhoto = true; // Устанавливаем, что фото успешно
            StartCoroutine(StartAnimationWithDelay());
        }
    }

    private System.Collections.IEnumerator StartAnimationWithDelay()
    {
        isSliding = true; // Устанавливаем флаг, чтобы избежать повторного запуска

        // Меняем изображение перед анимацией
        if (displayImage != null && imageList.Length > 0)
        {
            displayImage.sprite = imageList[Mathf.Clamp(index, 0, imageList.Length - 1)];
        }

        // Ждем перед началом анимации
        yield return new WaitForSeconds(delayBeforeAnimation);

        // Затемняем фон резко
        if (backgroundDimmer != null)
        {
            backgroundDimmer.color = dimColor; // Устанавливаем цвет с заданной прозрачностью
        }

        // Начинаем анимацию
        yield return StartCoroutine(SlideIn());
    }

    private System.Collections.IEnumerator SlideIn()
    {
        float elapsedTime = 0;

        while (elapsedTime < animationDuration)
        {
            // Линейная интерполяция между startPosition и endPosition
            uiElement.anchoredPosition = Vector2.Lerp(startPosition, endPosition, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Устанавливаем точное конечное положение
        uiElement.anchoredPosition = endPosition;
        isSliding = false; // Сбрасываем флаг
        FindObjectOfType<CommentDisplayManager>()?.ShowComments(index);
    }
}
