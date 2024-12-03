using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    public float moveSpeed = 5f; // Скорость перемещения камеры

    // Границы перемещения камеры
    public float minX = -10f;
    public float maxX = 10f;
    public float minY = -5f;
    public float maxY = 5f;

    private Vector3 moveDirection = Vector3.zero; // Направление движения
    private bool isMoving = false; // Флаг для движения

    public bool isEnabled = true; // Флаг для активации/деактивации скрипта

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(1f, 2f, -10f);
    }

    void Update()
    {
        // Если скрипт отключён, пропускаем обработку
        if (!isEnabled)
        {
            return;
        }

        // Получаем значения осей ввода
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            HandleMovementInputKEY(horizontal, vertical);
        }
        else if (isMoving)
        {
            HandleMovementInputUI();
        }
    }

    void HandleMovementInputKEY(float horizontal, float vertical)
    {
        // Рассчитываем новое положение камеры
        Vector3 newPosition = transform.position + new Vector3(horizontal, vertical, 0) * moveSpeed * Time.deltaTime;

        // Ограничиваем новое положение камеры по заданным границам
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        // Применяем ограниченное положение к камере
        transform.position = newPosition;
    }

    void HandleMovementInputUI()
    {
        // Рассчитываем новое положение камеры
        Vector3 newPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;

        // Ограничиваем новое положение камеры по заданным границам
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        // Применяем ограниченное положение к камере
        transform.position = newPosition;
    }

    // Методы для установки направления движения
    public void MoveUp()
    {
        if (!isEnabled) return; // Проверяем, активен ли скрипт
        moveDirection = Vector3.up;
        isMoving = true;
    }

    public void MoveDown()
    {
        if (!isEnabled) return; // Проверяем, активен ли скрипт
        moveDirection = Vector3.down;
        isMoving = true;
    }

    public void MoveLeft()
    {
        if (!isEnabled) return; // Проверяем, активен ли скрипт
        moveDirection = Vector3.left;
        isMoving = true;
    }

    public void MoveRight()
    {
        if (!isEnabled) return; // Проверяем, активен ли скрипт
        moveDirection = Vector3.right;
        isMoving = true;
    }

    // Метод для остановки движения
    public void StopMoving()
    {
        if (!isEnabled) return; // Проверяем, активен ли скрипт
        isMoving = false;
        moveDirection = Vector3.zero;
    }
}
