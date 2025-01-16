using TMPro;
using UnityEngine;

public class TextChangeOnClick : MonoBehaviour
{
    public TMP_Text textMeshPro;  // Ссылка на компонент TextMeshPro
    public int requiredClicks = 5;  // Количество нажатий, после которых меняется текст
    private int clickCount = 0;     // Счетчик нажатий
    public string Text;

    // Этот метод будет вызываться при нажатии на объект
    public void OnButtonClick()
    {
        clickCount++;

        // Проверяем, достиг ли счетчик требуемого количества нажатий
        if (clickCount >= requiredClicks)
        {
            // Меняем текст
            textMeshPro.text = Text;
        }
    }
}
