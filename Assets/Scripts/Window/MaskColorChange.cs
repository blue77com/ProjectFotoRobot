using UnityEngine;

public class MaskColorChange : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, что это маска
        if (other.GetComponent<SpriteMask>() != null)
        {
            // Меняем цвет объекта при пересечении с маской
            spriteRenderer.color = Color.yellow; // Цвет, в который вы хотите изменить
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<SpriteMask>() != null)
        {
            // Возвращаем цвет объекта при выходе из маски
            spriteRenderer.color = Color.black; // Исходный цвет
        }
    }
}
