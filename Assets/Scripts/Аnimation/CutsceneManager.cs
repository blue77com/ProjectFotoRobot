using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public FadeController fadeController;
    public Button nextButton;
    public int[] maxVisibleImages;
    public string nextSceneName;
    public FlashSceneTransition flashSceneTransition;
    public FlashFadeOut flashFadeOut;

    private int currentImageIndex = 0;
    private int currentGroupStartIndex = 0;
    private int currentMaxVisibleCount = 0;
    public bool isAnimate = false;
    private bool isAnimating = false;
    private bool isHandlingNextImage;

    public void StartCutscene()
    {
        if (maxVisibleImages == null || maxVisibleImages.Length == 0)
        {
            Debug.LogError("Массив maxVisibleImages не задан!");
            return;
        }

        currentMaxVisibleCount = maxVisibleImages[0];
        fadeController.ShowGroup(currentImageIndex);
        isAnimating = isAnimate;
        nextButton.onClick.AddListener(OnNextButtonClicked);
    }

    private void OnNextButtonClicked()
    {
        if (isAnimating)
        {
            fadeController.SkipCurrentAnimation(currentImageIndex); // Пропуск текущей анимации
            isAnimating = false; // Сбрасываем флаг, так как анимация пропущена
            return;
        }

        StartCoroutine(HandleNextImage());
    }

    private IEnumerator HandleNextImage()
    {
        if (isHandlingNextImage) yield break; // Предотвращаем повторное выполнение
        isHandlingNextImage = true;

        currentImageIndex++;

        if (currentImageIndex < fadeController.GetTotalGroups())
        {
            if (currentImageIndex - currentGroupStartIndex >= currentMaxVisibleCount)
            {
                isAnimating = true;

                // Пропуск анимации скрытия, если пользователь нажал "Далее"
                fadeController.SkipHideGroup(currentGroupStartIndex, currentGroupStartIndex + currentMaxVisibleCount);
                currentGroupStartIndex = currentImageIndex;
                int groupIndex = Mathf.Min(currentGroupStartIndex / currentMaxVisibleCount, maxVisibleImages.Length - 1);
                currentMaxVisibleCount = maxVisibleImages[groupIndex];
                isAnimating = false;
            }

            isAnimating = isAnimate;
            fadeController.ShowGroup(currentImageIndex);
            if (isAnimating) 
            {
                yield return new WaitForSeconds(fadeController.GetTotalGroups());
            }
            // Ждём завершения анимации появления
            isAnimating = false; // Сбрасываем флаг после завершения анимации
        }
        else
        {
            // Если это последняя картинка, переходим на следующую сцену
            if (!string.IsNullOrEmpty(nextSceneName))
            {
                if (flashFadeOut != null)
                {
                    FlashFadeOut.hasRun = false;
                }
                else
                {
                    Debug.LogWarning("CutsceneManager не назначен или отсутствует в сцене.");
                }

                flashSceneTransition.TriggerFlashAndSwitchScene(nextSceneName);
            }
            else
            {
                Debug.LogError("Название следующей сцены не задано!");
            }
        }

        isHandlingNextImage = false;
    }

}
