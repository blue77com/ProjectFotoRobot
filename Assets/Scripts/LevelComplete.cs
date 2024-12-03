using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    [SerializeField] private int itemCount; // Количество элементов
    [SerializeField] private GameObject completionUI; // UI элемент для завершения
    [SerializeField] private string nextSceneName; // Имя следующей сцены

    private bool allItemsMarked = false;

    void Start()
    {
        // Изначально UI отключён
        if (completionUI != null)
        {
            completionUI.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Completion UI не настроен!");
        }
    }

    void Update()
    {
        // Проверяем состояние только если оно ещё не было выполнено
        if (!allItemsMarked && AllItemsMarked())
        {
            ShowCompletionUI();
        }
    }

    private bool AllItemsMarked()
    {
        // Проверяем, есть ли все индексы от 0 до itemCount-1 в SharedState.VisitedIndices
        for (int i = 0; i < itemCount; i++)
        {
            if (!SharedState.VisitedIndices.Contains(i)) return false; // Если хотя бы один индекс отсутствует, возвращаем false
        }

        return true; // Все индексы присутствуют
    }

    private void ShowCompletionUI()
    {
        allItemsMarked = true; // Состояние выполнено
        if (completionUI != null)
        {
            completionUI.SetActive(true); // Показываем UI элемент
        }
    }

    // Метод для перехода на следующую сцену
    public void TransitionToNextScene()
    {
        Debug.Log("Переход на следующую сцену...");
        SceneManager.LoadScene(nextSceneName); // Переход на следующую сцену
    }

    // Метод для сброса состояния
    public void ResetLevelState()
    {
        SharedState.VisitedIndices.Clear(); // Очищаем HashSet
        Debug.Log("Состояние уровня сброшено.");
    }
}
