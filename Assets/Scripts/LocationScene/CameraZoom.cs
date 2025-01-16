using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomStep = 0.5f; // ��� ��������� ��������
    public float minZoom = 2f;    // ����������� �������� ��������
    public float maxZoom = 10f;   // ������������ �������� ��������

    public float minX = -10f; // ������� �����
    public float maxX = 10f;
    public float minY = -5f;
    public float maxY = 5f;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        if (cam == null || !cam.orthographic)
        {
            Debug.LogError("This script requires an orthographic camera.");
        }
    }

    void Update()
    {
        if (cam == null) return;

        // ���������� ��������
        if (Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetKeyDown(KeyCode.Equals))
        {
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - zoomStep, minZoom, maxZoom);
            AdjustCameraPosition(); // ���������� ������ ����� ��������� ��������
        }

        // ���������� ��������
        if (Input.GetKeyDown(KeyCode.KeypadMinus) || Input.GetKeyDown(KeyCode.Minus))
        {
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize + zoomStep, minZoom, maxZoom);
            AdjustCameraPosition(); // ���������� ������ ����� ��������� ��������
        }
    }

    void AdjustCameraPosition()
    {
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        Vector3 currentPos = cam.transform.position;

        float clampedX = Mathf.Clamp(currentPos.x, minX + camWidth / 2, maxX - camWidth / 2);
        float clampedY = Mathf.Clamp(currentPos.y, minY + camHeight / 2, maxY - camHeight / 2);

        // ������� ����������� ������
        Vector3 targetPosition = new Vector3(clampedX, clampedY, currentPos.z);
        cam.transform.position = Vector3.Lerp(cam.transform.position, targetPosition, 5f); // �����������
    }

}
