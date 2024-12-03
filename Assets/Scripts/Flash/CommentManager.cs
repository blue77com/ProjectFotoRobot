using UnityEngine;
using TMPro;

public class CommentManager : MonoBehaviour
{
    public FlashEffect flashEffect; // ������ �� ������ �������
    public TextMeshProUGUI commentDisplay; // TextMeshProUGUI ������� ��� ����������� �����������
    public string defaultComment = "������ �����������..."; // ����������� �� ����� �������
    public string winComment; // ������������� ����������� � ������ ��������
    public string[] loseComments; // ������ ������������ � ������ ���������

    public InfiniteScrollFrameAnimation ScrollFrameAnimation;
    public CameraFOVController cameraFOVController;
    public CameraMoving cameraMovingScript;

    public float defaultCommentDuration = 2.0f; // ������������ ������ ������������� ������

    private bool isCommentVisible = false;

    private void Update()
    {
        // ������� ������� ��������� ������� � �����������
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
            // ���������� ����������� ����������� ��� ������ �������
            commentDisplay.text = defaultComment;
            commentDisplay.gameObject.SetActive(true); // ������ ����� �������
            isCommentVisible = true;

            // ��������� ��������� ����� ���������� ������� � �������������� ���������
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
            commentDisplay.text = winComment; // ���������� ������������� �����������
        }
        isCommentVisible = true;
    }

    private void ShowLoseComment()
    {
        if (commentDisplay != null && loseComments.Length > 0)
        {
            // ���������� ��������� ����������� �� �������
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
