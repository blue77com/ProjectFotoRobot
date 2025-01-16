using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Объект, за которым следует камера
    public float smoothTime = 0.2f; // Время сглаживания
    public float minX = -10f; // Границы карты
    public float maxX = 10f;
    public float minY = -5f;
    public float maxY = 5f;

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

        // Размеры камеры
        Camera cam = Camera.main;
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        // Положение цели
        Vector3 desiredPosition = target.position;

        // Ограничиваем цель камеры
        float clampedX = Mathf.Clamp(desiredPosition.x, minX + camWidth / 2, maxX - camWidth / 2);
        float clampedY = Mathf.Clamp(desiredPosition.y, minY + camHeight / 2, maxY - camHeight / 2);

        // Ограниченное положение камеры
        Vector3 clampedPosition = new Vector3(clampedX, clampedY, transform.position.z);

        // Плавное движение камеры
        transform.position = Vector3.SmoothDamp(transform.position, clampedPosition, ref velocity, smoothTime);
    }
}
