using UnityEngine;

public class DisableSpriteMaskOnCollision : MonoBehaviour
{
    // ������ �� SpriteMask, ������� ����� ���������
    public SpriteMask spriteMask;

    void Start()
    {
        if (spriteMask.enabled == false)
        {
            spriteMask.enabled = true;  // �������� Sprite Mask ��� ������, ���� �� ��� ��������
        }
    }
    // ���� ����� ����� ������ ��� ������������
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player2"))
        {
            spriteMask.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player2"))
        {
            spriteMask.enabled = true;
        }
    }

}
