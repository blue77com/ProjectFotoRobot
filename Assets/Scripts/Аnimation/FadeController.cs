using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    public CanvasGroup[] canvasGroups; // ������ CanvasGroup ��� ���������� ���������� UI
    public float fadeDuration = 1f;

    private Dictionary<CanvasGroup, Coroutine> activeCoroutines = new Dictionary<CanvasGroup, Coroutine>();

    // �������� CanvasGroup � ������� ����������
    public void ShowGroup(int index)
    {
        if (index >= 0 && index < canvasGroups.Length)
        {
            StopActiveAnimation(canvasGroups[index]);
            activeCoroutines[canvasGroups[index]] = StartCoroutine(FadeIn(canvasGroups[index]));
        }
    }

    // ������ ������ CanvasGroup � ������� ����������
    public IEnumerator HideGroup(int startIndex, int endIndex)
    {
        for (int i = startIndex; i < endIndex && i < canvasGroups.Length; i++)
        {
            StopActiveAnimation(canvasGroups[i]);
            activeCoroutines[canvasGroups[i]] = StartCoroutine(FadeOut(canvasGroups[i]));
        }

        yield return new WaitForSeconds(fadeDuration);
    }

    // ���������� ������� ������ CanvasGroup ��� ��������
    public bool SkipHideGroup(int startIndex, int endIndex)
    {
        bool groupSkipped = false;

        for (int i = startIndex; i < endIndex && i < canvasGroups.Length; i++)
        {
            if (canvasGroups[i].gameObject.activeSelf)
            {
                StopActiveAnimation(canvasGroups[i]);
                SetAlpha(canvasGroups[i], 0);
                canvasGroups[i].gameObject.SetActive(false);
                groupSkipped = true;
            }
        }

        return groupSkipped;
    }

    // ���������� ������� �������� � ����������� ����������� CanvasGroup
    public void SkipCurrentAnimation(int index)
    {
        if (index >= 0 && index < canvasGroups.Length)
        {
            StopActiveAnimation(canvasGroups[index]);
            SetAlpha(canvasGroups[index], 1);
            canvasGroups[index].gameObject.SetActive(true);
        }
    }

    private void StopActiveAnimation(CanvasGroup group)
    {
        if (activeCoroutines.ContainsKey(group) && activeCoroutines[group] != null)
        {
            StopCoroutine(activeCoroutines[group]);
            activeCoroutines.Remove(group);
        }
    }

    private IEnumerator FadeIn(CanvasGroup group)
    {
        SetAlpha(group, 0);
        group.gameObject.SetActive(true);

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            SetAlpha(group, t / fadeDuration);
            yield return null;
        }

        SetAlpha(group, 1);
        activeCoroutines.Remove(group);
    }

    private IEnumerator FadeOut(CanvasGroup group)
    {
        SetAlpha(group, 1);

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            SetAlpha(group, 1 - t / fadeDuration);
            yield return null;
        }

        SetAlpha(group, 0);
        group.gameObject.SetActive(false);
        activeCoroutines.Remove(group);
    }

    private void SetAlpha(CanvasGroup group, float alpha)
    {
        group.alpha = Mathf.Clamp01(alpha);
        group.interactable = alpha > 0.5f; // ��������� �������� ������ ��� ������������ ���������
        group.blocksRaycasts = alpha > 0f; // ���������� ������ ������ ��� ������� ��������
    }

    public int GetTotalGroups()
    {
        return canvasGroups.Length;
    }
}
