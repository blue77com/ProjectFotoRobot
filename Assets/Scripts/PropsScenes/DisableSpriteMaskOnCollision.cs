using UnityEngine;

public class DisableSpriteMaskOnCollision : MonoBehaviour
{
    // —сылка на SpriteMask, который нужно отключить
    public SpriteMask spriteMask;

    void Start()
    {
        if (spriteMask.enabled == false)
        {
            spriteMask.enabled = true;  // ¬ключаем Sprite Mask при старте, если он был выключен
        }
    }
    // Ётот метод будет вызван при столкновении
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
