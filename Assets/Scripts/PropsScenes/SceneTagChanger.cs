using UnityEngine;

public class SceneTagChanger : MonoBehaviour
{
    public string newTag = "Player2"; // ����� ���, ������� ����� �������� ��������

    void Start()
    {
        // ������� ���� �������� � ����� "Player", ����� �������� �� ����
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Props"))
        {
            // ���� ������ ��� ������� (������ ���� � ������)
            if (SharedState.VisitedIndices.Contains(obj.GetComponent<SceneTransitionWithCullingMask>().indexPoint))
            {
                // �������� ��� �������
                obj.tag = newTag;
            }
        }
    }
}
