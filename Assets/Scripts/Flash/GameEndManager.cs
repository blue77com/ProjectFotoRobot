using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEndManager : MonoBehaviour
{
    public GameObject uiBlocker; // UI элемент, блокирующий взаимодействие (например, прозрачный Panel)
    public Button nextSceneButton; // Кнопка для перехода на следующую сцену
    public string nextSceneName = "NextScene"; // Имя следующей сцены
    private bool isGameEnd = false; // Флаг, указывающий, что игра завершена
    public static int layerNumber;
    public ColorChanger colorChanger;
    private void Start()
    {
        // Убедимся, что UI блокер и кнопка отключены в начале
        if (uiBlocker != null)
            uiBlocker.SetActive(false);

        if (nextSceneButton != null)
        {
            nextSceneButton.gameObject.SetActive(false);
            nextSceneButton.onClick.AddListener(LoadNextScene);
        }
    }

    public void CheckAndEndGame()
    {
        // Проверяем завершение всех анимаций в PhotoCaptureManager
        var photoManager = FindObjectOfType<CommentDisplayManager>();
        if (photoManager != null && photoManager.IsAnimationComplete() && !isGameEnd)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        isGameEnd = true;

        // Блокируем UI
        if (uiBlocker != null)
            uiBlocker.SetActive(true);

        // Ожидание перед появлением кнопки
        StartCoroutine(ShowButtonWithDelay());
    }

    private System.Collections.IEnumerator ShowButtonWithDelay()
    {
        // Подождем немного (можно настроить время, если нужно)
        yield return new WaitForSeconds(0.5f);

        // Активируем кнопку перехода на следующую сцену
        if (nextSceneButton != null)
            nextSceneButton.gameObject.SetActive(true);

        colorChanger.MarkObjectByIndex(layerNumber);
    }

    private void Update()
    {
        if (isGameEnd)
        {
            // Проверяем, нажата ли клавиша Backspace
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                LoadNextScene();
            }
        }
    }

    private void LoadNextScene()
    {
        // Загружаем следующую сцену
        SceneManager.LoadScene(nextSceneName);

    }
}
