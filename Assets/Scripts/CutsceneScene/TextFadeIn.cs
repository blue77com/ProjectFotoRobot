using UnityEngine;
using TMPro;
using System.Collections;

public class TextFadeIn : MonoBehaviour
{
    public TextMeshProUGUI text;  // ������ �� ��������� TextMeshProUGUI
    public float fadeDuration = 2f; // ������������ ��������
    public float delayBeforeFade = 1f; // �������� ����� ������� ��������

    private void Start()
    {
        if (text == null)
            text = GetComponent<TextMeshProUGUI>();

        StartCoroutine(FadeInText());
    }

    private IEnumerator FadeInText()
    {
        text.alpha = 0; // ������������� ��������� ������������ ������

        // �������� ����� ������� ��������
        yield return new WaitForSeconds(delayBeforeFade);

        float elapsedTime = 0;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            text.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null; // ���� ���������� �����
        }

        text.alpha = 1; // ������������� ������ ���������
    }
}
