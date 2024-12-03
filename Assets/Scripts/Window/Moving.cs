using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Скорость перемещения камеры

    // Границы перемещения камеры
    public float minX = -10f;
    public float maxX = 10f;
    public float minY = -5f;
    public float maxY = 5f;

    void Update()
    {
        HandleMovementInput();
    }

    void HandleMovementInput()
    {
        // Получаем значения осей ввода
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Рассчитываем новое положение камеры
        Vector3 newPosition = transform.position + new Vector3(horizontal, vertical, 0) * moveSpeed * Time.deltaTime;

        // Ограничиваем новое положение камеры по заданным границам
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        // Применяем ограниченное положение к камере
        transform.position = newPosition;
    }
}
