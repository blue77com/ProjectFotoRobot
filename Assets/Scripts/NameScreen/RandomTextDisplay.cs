using TMPro;
using UnityEngine;

public class RandomTextDisplay : MonoBehaviour
{
    public TMP_Text textMeshPro;  // Ссылка на компонент TextMeshPro
    public string[] textList;     // Массив текстов, из которых будет выбирать случайный

    // Этот метод будет вызываться для отображения случайного текста
    private void Start()
    {
        DisplayRandomText();
    }
    public void DisplayRandomText()
    {
        if (textList.Length > 0)
        {
            // Выбираем случайный индекс из списка
            int randomIndex = Random.Range(0, textList.Length);

            // Меняем текст в TextMesh Pro на случайный из списка
            textMeshPro.text = textList[randomIndex];
        }
    }
}
