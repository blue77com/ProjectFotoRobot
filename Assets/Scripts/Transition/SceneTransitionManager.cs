using UnityEngine;

public class SceneTransitionManager : MonoBehaviour
{
    public static LayerMask TargetCullingMask; // Статическая переменная для хранения culling mask
    public static string TargetCameraName; // Статическая переменная для хранения имени камеры
    void Start()
    {
        // Находим камеру по имени
        GameObject cameraObject = GameObject.Find(TargetCameraName);
        if (cameraObject != null)
        {
            Camera targetCamera = cameraObject.GetComponent<Camera>();
            if (targetCamera != null)
            {
                // Применяем culling mask к найденной камере
                targetCamera.cullingMask = TargetCullingMask;

                // Получаем целевой слой из culling mask и применяем его к камере и дочерним объектам
                int targetLayer = LayerMaskToLayer(TargetCullingMask);
                if (targetLayer != -1)
                {
                    // Устанавливаем слой для камеры и всех её дочерних объектов
                    SetLayerRecursively(cameraObject, targetLayer);
                }
                else
                {
                    Debug.LogWarning("Culling mask не соответствует конкретному слою.");
                }
            }
            else
            {
                Debug.LogWarning("Компонент Camera не найден на объекте " + TargetCameraName);
            }
        }
        else
        {
            Debug.LogWarning("Камера с именем " + TargetCameraName + " не найдена в сцене.");
        }
    }

    // Метод для преобразования culling mask в конкретный слой
    private int LayerMaskToLayer(LayerMask mask)
    {
        int layer = 0;
        int maskValue = mask.value;
        while (maskValue > 1)
        {
            maskValue = maskValue >> 1;
            layer++;
        }
        return maskValue == 1 ? layer : -1; // Возвращаем слой, если mask соответствует одному слою
    }

    // Метод для рекурсивного задания слоя объекту и всем его дочерним объектам
    private void SetLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, layer);
        }
    }
}