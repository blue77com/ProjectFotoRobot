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
            Debug.LogError("������ maxVisibleImages �� �����!");
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
            fadeController.SkipCurrentAnimation(currentImageIndex); // ������� ������� ��������
            isAnimating = false; // ���������� ����, ��� ��� �������� ���������
            return;
        }

        StartCoroutine(HandleNextImage());
    }

    private IEnumerator HandleNextImage()
    {
        if (isHandlingNextImage) yield break; // ������������� ��������� ����������
        isHandlingNextImage = true;

        currentImageIndex++;

        if (currentImageIndex < fadeController.GetTotalGroups())
        {
            if (currentImageIndex - currentGroupStartIndex >= currentMaxVisibleCount)
            {
                isAnimating = true;

                // ������� �������� �������, ���� ������������ ����� "�����"
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
            // ��� ���������� �������� ���������
            isAnimating = false; // ���������� ���� ����� ���������� ��������
        }
        else
        {
            // ���� ��� ��������� ��������, ��������� �� ��������� �����
            if (!string.IsNullOrEmpty(nextSceneName))
            {
                if (flashFadeOut != null)
                {
                    FlashFadeOut.hasRun = false;
                }
                else
                {
                    Debug.LogWarning("CutsceneManager �� �������� ��� ����������� � �����.");
                }

                flashSceneTransition.TriggerFlashAndSwitchScene(nextSceneName);
            }
            else
            {
                Debug.LogError("�������� ��������� ����� �� ������!");
            }
        }

        isHandlingNextImage = false;
    }

}
