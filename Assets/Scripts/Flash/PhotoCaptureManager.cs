using UnityEngine;
using UnityEngine.UI;

public class PhotoCaptureManager : MonoBehaviour
{
    public FlashEffect flashEffect; // ������ �� ������ �������
    public RectTransform uiElement; // ������ �� UI �������
    public Image backgroundDimmer; // ������ �� Image ������������ ����
    public Image displayImage; // ������ �� UI Image ��� ����� �����������
    public Sprite[] imageList; // ������ �����������
    public Vector2 startPosition; // ��������� �������
    public Vector2 endPosition; // �������� �������
    public float animationDuration = 1.0f; // ������������ ��������
    public float delayBeforeAnimation = 0.5f; // �������� ����� ������� ��������
    public Color dimColor = new Color(0, 0, 0, 0.5f); // ���� ���������� (������ � ����� = 0.5)

    public bool hasSuccessfulPhoto = false; // ���� ��������� ����
    private bool isSliding = false; // ���� ��� �������������� ���������� ������� ��������
    public static int index;

    private void Start()
    {
        // ������������� ��������� ������� ��������
        uiElement.anchoredPosition = startPosition;

        // ������������� ��������� ������������ ������������ ����
        if (backgroundDimmer != null)
        {
            backgroundDimmer.color = new Color(dimColor.r, dimColor.g, dimColor.b, 0); // ����������
        }

        // ������������� ��������� �����������
        if (displayImage != null && imageList.Length > 0)
        {
            displayImage.sprite = imageList[Mathf.Clamp(index, 0, imageList.Length - 1)];
        }
    }

    public void UpdateAfterFlash(bool IsWin)
    {
        if (IsWin && !isSliding && !hasSuccessfulPhoto)
        {
            hasSuccessfulPhoto = true; // �������������, ��� ���� �������
            StartCoroutine(StartAnimationWithDelay());
        }
    }

    private System.Collections.IEnumerator StartAnimationWithDelay()
    {
        isSliding = true; // ������������� ����, ����� �������� ���������� �������

        // ������ ����������� ����� ���������
        if (displayImage != null && imageList.Length > 0)
        {
            displayImage.sprite = imageList[Mathf.Clamp(index, 0, imageList.Length - 1)];
        }

        // ���� ����� ������� ��������
        yield return new WaitForSeconds(delayBeforeAnimation);

        // ��������� ��� �����
        if (backgroundDimmer != null)
        {
            backgroundDimmer.color = dimColor; // ������������� ���� � �������� �������������
        }

        // �������� ��������
        yield return StartCoroutine(SlideIn());
    }

    private System.Collections.IEnumerator SlideIn()
    {
        float elapsedTime = 0;

        while (elapsedTime < animationDuration)
        {
            // �������� ������������ ����� startPosition � endPosition
            uiElement.anchoredPosition = Vector2.Lerp(startPosition, endPosition, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ������������� ������ �������� ���������
        uiElement.anchoredPosition = endPosition;
        isSliding = false; // ���������� ����
        FindObjectOfType<CommentDisplayManager>()?.ShowComments(index);
    }
}
