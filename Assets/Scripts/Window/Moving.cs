using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // �������� ����������� ������

    // ������� ����������� ������
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
        // �������� �������� ���� �����
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // ������������ ����� ��������� ������
        Vector3 newPosition = transform.position + new Vector3(horizontal, vertical, 0) * moveSpeed * Time.deltaTime;

        // ������������ ����� ��������� ������ �� �������� ��������
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        // ��������� ������������ ��������� � ������
        transform.position = newPosition;
    }
}
