using UnityEngine;

public class StateManager : MonoBehaviour
{
    private const string SaveKey = "MarkedObjects";

    // Сохранить состояние списка в виде строки
    public void SaveState(string state)
    {
        PlayerPrefs.SetString(SaveKey, state);
        PlayerPrefs.Save();
    }

    // Загрузить состояние списка
    public string LoadState(int itemCount)
    {
        if (!PlayerPrefs.HasKey(SaveKey))
        {
            // Если состояние отсутствует, создаём пустое
            string initialState = new string('0', itemCount);
            SaveState(initialState);
            return initialState;
        }

        // Загружаем существующее сохранение
        return PlayerPrefs.GetString(SaveKey);
    }

    // Удалить сохраненное состояние (для тестирования или сброса)
    public void ClearState()
    {
        PlayerPrefs.DeleteKey(SaveKey);
    }
}
