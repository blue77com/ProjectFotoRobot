using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public Canvas canvas; // Ссылка на Canvas
    private static int activationCount = 0; // Счетчик для отслеживания вызовов

    // Функция для активации Canvas
    public void ShowCanvas()
    {
        if (activationCount < 2) // Проверка, сработал ли уже дважды
        {
            if (canvas != null)
            {
                canvas.gameObject.SetActive(true); // Включаем Canvas
                Time.timeScale = 0f; // Ставим игру на паузу
            }
        }
    }

    // Функция для деактивации Canvas
    public void HideCanvas()
    {
        if (activationCount < 2) // Проверка, сработал ли уже дважды
        {
            if (canvas != null)
            {
                canvas.gameObject.SetActive(false); // Выключаем Canvas
                Time.timeScale = 1f; // Возвращаем нормальную скорость игры
                activationCount++; // Увеличиваем счетчик
            }
        }
    }
}
