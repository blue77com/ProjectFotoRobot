using UnityEngine;
using TMPro;

public class CommentManager : MonoBehaviour
{
    public FlashEffect flashEffect; // Ссылка на скрипт вспышки
    public TextMeshProUGUI commentDisplay; // TextMeshProUGUI элемент для отображения комментария
    public string defaultComment = "Снимок выполняется..."; // Комментарий во время вспышки
    public string winComment; // Фиксированный комментарий в случае выигрыша
    public string[] loseComments; // Массив комментариев в случае проигрыша

    public InfiniteScrollFrameAnimation ScrollFrameAnimation;
    public CameraFOVController cameraFOVController;
    public CameraMoving cameraMovingScript;

    public float defaultCommentDuration = 2.0f; // Длительность показа перебивочного текста

    private bool isCommentVisible = false;

    private void Update()
    {
        // Нажатие пробела запускает вспышку и комментарий
        if (Input.GetKeyDown(KeyCode.Space) && !isCommentVisible)
        {
            TriggerComment();
        }
    }

    public void TriggerComment()
    {
        StartFlashComment();
    }

    private void StartFlashComment()
    {
        if (commentDisplay != null)
        {
            // Показываем стандартный комментарий при старте вспышки
            commentDisplay.text = defaultComment;
            commentDisplay.gameObject.SetActive(true); // Делаем текст видимым
            isCommentVisible = true;

            // Проверяем результат после завершения вспышки с дополнительной задержкой
            float totalDelay = flashEffect.flashDuration + flashEffect.flashInstantDuration + defaultCommentDuration;
            Invoke(nameof(UpdateCommentAfterFlash), totalDelay);
        }
    }

    private void UpdateCommentAfterFlash()
    {
        if (!isCommentVisible) return;

        if (flashEffect.IsWin)
        {
            ShowWinComment();
        }
        else
        {
            ShowLoseComment();
        }

    }

    private void ShowWinComment()
    {
        if (commentDisplay != null)
        {
            commentDisplay.text = winComment; // Отображаем фиксированный комментарий
        }
        isCommentVisible = true;
    }

    private void ShowLoseComment()
    {
        if (commentDisplay != null && loseComments.Length > 0)
        {
            // Отображаем случайный комментарий из массива
            string randomComment = loseComments[Random.Range(0, loseComments.Length)];
            commentDisplay.text = randomComment;
        }
        isCommentVisible = false;
        cameraMovingScript.isEnabled = true;
        cameraFOVController.isEnabled = true;
        ScrollFrameAnimation.isEnabled = true;
        flashEffect.isButton = false;


    }

}
