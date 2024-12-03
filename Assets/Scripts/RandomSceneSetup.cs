using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class RandomSceneSetup : MonoBehaviour
{
    public GameObject targetObject; // Объект для изменения позиции
    public Vector2 positionRangeX; // Диапазон по оси X
    public Vector2 positionRangeY; // Диапазон по оси Y

    public Camera targetCamera; // Камера для изменения FOV
    public Vector2 fovRange; // Диапазон значений FOV

    public PostProcessVolume postProcessVolume; // Постобработка для DOF
    public Vector2 dofRange; // Диапазон значений DOF

    private void Start()
    {
        // Генерация случайного положения объекта
        if (targetObject != null)
        {
            float randomX = Random.Range(positionRangeX.x, positionRangeX.y);
            float randomY = Random.Range(positionRangeY.x, positionRangeY.y);
            Vector3 newPosition = new Vector3(randomX, randomY, targetObject.transform.position.z);
            targetObject.transform.position = newPosition;

            Debug.Log($"Object Position: {newPosition}");
        }

        // Генерация случайного значения FOV для камеры
        if (targetCamera != null)
        {
            float randomFOV = Random.Range(fovRange.x, fovRange.y);
            targetCamera.fieldOfView = randomFOV;

            Debug.Log($"Camera FOV: {randomFOV}");
        }

        // Генерация случайного значения DOF
        if (postProcessVolume != null && postProcessVolume.profile.HasSettings<DepthOfField>())
        {
            DepthOfField dof;
            if (postProcessVolume.profile.TryGetSettings(out dof))
            {
                float randomDOF = Random.Range(dofRange.x, dofRange.y);
                dof.focusDistance.value = randomDOF;

                Debug.Log($"DOF Focus Distance: {randomDOF}");
            }
            else
            {
                Debug.LogWarning("DepthOfField settings not found in the PostProcessVolume.");
            }
        }
    }
}
