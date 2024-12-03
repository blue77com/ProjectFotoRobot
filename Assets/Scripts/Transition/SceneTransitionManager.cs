using UnityEngine;

public class SceneTransitionManager : MonoBehaviour
{
    public static LayerMask TargetCullingMask; // ����������� ���������� ��� �������� culling mask
    public static string TargetCameraName; // ����������� ���������� ��� �������� ����� ������
    void Start()
    {
        // ������� ������ �� �����
        GameObject cameraObject = GameObject.Find(TargetCameraName);
        if (cameraObject != null)
        {
            Camera targetCamera = cameraObject.GetComponent<Camera>();
            if (targetCamera != null)
            {
                // ��������� culling mask � ��������� ������
                targetCamera.cullingMask = TargetCullingMask;

                // �������� ������� ���� �� culling mask � ��������� ��� � ������ � �������� ��������
                int targetLayer = LayerMaskToLayer(TargetCullingMask);
                if (targetLayer != -1)
                {
                    // ������������� ���� ��� ������ � ���� � �������� ��������
                    SetLayerRecursively(cameraObject, targetLayer);
                }
                else
                {
                    Debug.LogWarning("Culling mask �� ������������� ����������� ����.");
                }
            }
            else
            {
                Debug.LogWarning("��������� Camera �� ������ �� ������� " + TargetCameraName);
            }
        }
        else
        {
            Debug.LogWarning("������ � ������ " + TargetCameraName + " �� ������� � �����.");
        }
    }

    // ����� ��� �������������� culling mask � ���������� ����
    private int LayerMaskToLayer(LayerMask mask)
    {
        int layer = 0;
        int maskValue = mask.value;
        while (maskValue > 1)
        {
            maskValue = maskValue >> 1;
            layer++;
        }
        return maskValue == 1 ? layer : -1; // ���������� ����, ���� mask ������������� ������ ����
    }

    // ����� ��� ������������ ������� ���� ������� � ���� ��� �������� ��������
    private void SetLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, layer);
        }
    }
}