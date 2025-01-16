using UnityEngine;

public class StateManager : MonoBehaviour
{
    private const string SaveKey = "MarkedObjects";

    // ��������� ��������� ������ � ���� ������
    public void SaveState(string state)
    {
        PlayerPrefs.SetString(SaveKey, state);
        PlayerPrefs.Save();
    }

    // ��������� ��������� ������
    public string LoadState(int itemCount)
    {
        if (!PlayerPrefs.HasKey(SaveKey))
        {
            // ���� ��������� �����������, ������ ������
            string initialState = new string('0', itemCount);
            SaveState(initialState);
            return initialState;
        }

        // ��������� ������������ ����������
        return PlayerPrefs.GetString(SaveKey);
    }

    // ������� ����������� ��������� (��� ������������ ��� ������)
    public void ClearState()
    {
        PlayerPrefs.DeleteKey(SaveKey);
    }
}
