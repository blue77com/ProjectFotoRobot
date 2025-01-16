using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // ������, �� ������� ������� ������
    public float smoothTime = 0.2f; // ����� �����������
    public float minX = -10f; // ������� �����
    public float maxX = 10f;
    public float minY = -5f;
    public float maxY = 5f;

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

        // ������� ������
        Camera cam = Camera.main;
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        // ��������� ����
        Vector3 desiredPosition = target.position;

        // ������������ ���� ������
        float clampedX = Mathf.Clamp(desiredPosition.x, minX + camWidth / 2, maxX - camWidth / 2);
        float clampedY = Mathf.Clamp(desiredPosition.y, minY + camHeight / 2, maxY - camHeight / 2);

        // ������������ ��������� ������
        Vector3 clampedPosition = new Vector3(clampedX, clampedY, transform.position.z);

        // ������� �������� ������
        transform.position = Vector3.SmoothDamp(transform.position, clampedPosition, ref velocity, smoothTime);
    }
}
