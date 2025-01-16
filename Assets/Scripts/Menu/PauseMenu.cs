using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu; // Ссылка на меню
    public Button firstButton;   // Кнопка "Продолжить"
    private bool isPaused = false;

    // Пределы области, в которой мышь будет работать (например, для меню)
    public RectTransform menuArea; // Прямоугольник области для меню

    void Start()
    {
        // Убедитесь, что меню скрыто в начале
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        // Открытие/закрытие меню по клавише Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        // Проверка, находится ли мышь над UI и управление фокусом
        if (isPaused)
        {
            if (IsMouseOverUI())
            {
                // Если мышь над UI, сбрасываем фокус с UI и останавливаем управление клавиатурой
                EventSystem.current.SetSelectedGameObject(null);
            }
            else
            {
                // Если мышь вышла за пределы UI, восстанавливаем фокус на первую кнопку
                if (EventSystem.current.currentSelectedGameObject == null)
                {
                    // Проверка на нажатие клавиши стрелки вверх, чтобы вернуть фокус на первую кнопку
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
                    }
                }
            }
        }
    }

    // Проверка, находится ли мышь в пределах UI и в пределах заданной области
    private bool IsMouseOverUI()
    {
        // Получаем позицию мыши в пикселях
        Vector2 mousePosition = Input.mousePosition;

        // Преобразуем мышь в пространство мировых координат
        if (!IsMouseInsideBounds(mousePosition))
        {
            return false; // Мышь за пределами разрешенной области
        }

        // Создаем объект PointerEventData, который содержит информацию о положении мыши
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
        {
            position = mousePosition
        };

        // Создаем список для хранения всех попаданий Raycast
        List<RaycastResult> raycastResults = new List<RaycastResult>();

        // Выполняем Raycast и записываем все результаты в список
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        // Если список не пустой, значит мышь находится в пределах UI элемента
        return raycastResults.Count > 0;
    }

    // Проверка, находится ли мышь внутри заданной области (menuArea)
    private bool IsMouseInsideBounds(Vector2 mousePosition)
    {
        // Преобразуем мышь в пространство координат RectTransform
        Vector2 localPoint = menuArea.InverseTransformPoint(mousePosition);

        // Проверяем, если позиция мыши внутри ограничений
        return menuArea.rect.Contains(localPoint);
    }

    public void Resume()
    {
        // Скрыть меню и возобновить игру
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void QuitGame()
    {
        // Выйти из игры
        Application.Quit();
    }

    void Pause()
    {
        // Показать меню и приостановить игру
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        // Устанавливаем фокус на первую кнопку при открытии меню
        EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
    }
}
