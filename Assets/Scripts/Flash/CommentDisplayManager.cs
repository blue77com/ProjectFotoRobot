using UnityEngine;
using TMPro;

public class CommentDisplayManager : MonoBehaviour
{
    public TextMeshProUGUI comment1; // ������ �� ������ ��������� ���������
    public TextMeshProUGUI comment2; // ������ �� ������ ��������� ���������
    public string[] commentsArray1; // ������ ������������ ��� ������� ������
    public string[] commentsArray2; // ������ ������������ ��� ������� ������
    public float delayBeforeShowingComments = 0.5f; // �������� ����� ������� ��������
    public float fadeDuration = 1.0f; // ������������ �������� ���������
    private bool isAnimationComplete = false; // ���� ���������� ���� ��������

    public void ShowComments(int index)
    {
        // ���������, ����� ������ ��� � �������� �������
        if (index < 0 || index >= commentsArray1.Length || index >= commentsArray2.Length)
        {
            Debug.LogWarning("������ ������� �� ������� �������.");
            return;
        }

        StartCoroutine(DisplayCommentsWithFade(index));
    }

    private System.Collections.IEnumerator DisplayCommentsWithFade(int index)
    {
        // ���� ����� ������� ��������
        yield return new WaitForSeconds(delayBeforeShowingComments);

        // �������� ����������� �� �������
        string selectedComment1 = commentsArray1[index];
        string selectedComment2 = commentsArray2[index];

        // ������������� ����� � ���������� TextMeshPro
        if (comment1 != null)
        {
            comment1.text = selectedComment1;
            yield return StartCoroutine(FadeInText(comment1)); // �������� ��������� ������� ������
        }

        if (comment2 != null)
        {
            comment2.text = selectedComment2;
            yield return StartCoroutine(FadeInText(comment2)); // �������� ��������� ������� ������
        }

        isAnimationComplete = true;

        FindObjectOfType<GameEndManager>()?.CheckAndEndGame();
    }

    private System.Collections.IEnumerator FadeInText(TextMeshProUGUI text)
    {
        float elapsedTime = 0;
        Color originalColor = text.color;

        // ������������� ��������� ������������ �� 0
        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);

        while (elapsedTime < fadeDuration)
        {
            // ���������� ����������� �����-�����
            float alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ������������� �����-����� �� 1 (��������� �������)
        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1);
    }

    public bool IsAnimationComplete()
    {
        return isAnimationComplete;
    }
}
