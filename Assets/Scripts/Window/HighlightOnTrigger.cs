using UnityEngine;

public class HighlightOnTrigger : MonoBehaviour
{
    private SpriteRenderer targetRenderer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, что объект имеет компонент SpriteRenderer
        targetRenderer = other.GetComponent<SpriteRenderer>();
        if (targetRenderer != null)
        {
            // Изменяем цвет при пересечении
            targetRenderer.color = Color.yellow; // Можно выбрать любой цвет для подсветки
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Возвращаем исходный цвет, если объект был подсвечен
        if (targetRenderer == null)
        {
            targetRenderer.color = Color.white; // Предполагается, что изначальный цвет - белый
            targetRenderer = null;
        }
    }
}
