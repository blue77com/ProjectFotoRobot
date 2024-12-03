using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    public float moveSpeed = 5f; // �������� ����������� ������

    // ������� ����������� ������
    public float minX = -10f;
    public float maxX = 10f;
    public float minY = -5f;
    public float maxY = 5f;

    private Vector3 moveDirection = Vector3.zero; // ����������� ��������
    private bool isMoving = false; // ���� ��� ��������

    public bool isEnabled = true; // ���� ��� ���������/����������� �������

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(1f, 2f, -10f);
    }

    void Update()
    {
        // ���� ������ ��������, ���������� ���������
        if (!isEnabled)
        {
            return;
        }

        // �������� �������� ���� �����
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
        // ������������ ����� ��������� ������
        Vector3 newPosition = transform.position + new Vector3(horizontal, vertical, 0) * moveSpeed * Time.deltaTime;

        // ������������ ����� ��������� ������ �� �������� ��������
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        // ��������� ������������ ��������� � ������
        transform.position = newPosition;
    }

    void HandleMovementInputUI()
    {
        // ������������ ����� ��������� ������
        Vector3 newPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;

        // ������������ ����� ��������� ������ �� �������� ��������
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        // ��������� ������������ ��������� � ������
        transform.position = newPosition;
    }

    // ������ ��� ��������� ����������� ��������
    public void MoveUp()
    {
        if (!isEnabled) return; // ���������, ������� �� ������
        moveDirection = Vector3.up;
        isMoving = true;
    }

    public void MoveDown()
    {
        if (!isEnabled) return; // ���������, ������� �� ������
        moveDirection = Vector3.down;
        isMoving = true;
    }

    public void MoveLeft()
    {
        if (!isEnabled) return; // ���������, ������� �� ������
        moveDirection = Vector3.left;
        isMoving = true;
    }

    public void MoveRight()
    {
        if (!isEnabled) return; // ���������, ������� �� ������
        moveDirection = Vector3.right;
        isMoving = true;
    }

    // ����� ��� ��������� ��������
    public void StopMoving()
    {
        if (!isEnabled) return; // ���������, ������� �� ������
        isMoving = false;
        moveDirection = Vector3.zero;
    }
}
