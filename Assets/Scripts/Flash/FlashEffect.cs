using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using System.Collections;

public class FlashEffect : MonoBehaviour
{
    public PhotoCaptureManager photoCaptureManager; // Ссылка на PhotoCaptureManager
    public GameObject flashObject; // Ссылка на объект вспышки (Quad или Sprite)
    public float flashDuration = 1.0f; // Длительность эффекта плавного исчезновения
    public float flashInstantDuration = 0.5f; // Длительность резкого появления
    [System.Serializable]
    public class FlashSettings
    {
        public Vector2 screenBoundsX; // Границы по оси X
        public Vector2 screenBoundsY; // Границы по оси Y
        public Vector2 fovRange; // Диапазон допустимого значения Field of View
        public Vector2 dofRange; // Диапазон допустимого значения Depth of Field
    }

    public FlashSettings[] flashSettingsByIndex; // Настройки для каждого индекса
    public Camera targetCamera; // Камера для проверки FOV и DOF

    public InfiniteScrollFrameAnimation ScrollFrameAnimation;
    public CameraFOVController cameraFOVController;
    public CameraMoving cameraMovingScript;

    public bool IsWin { get; private set; } // Состояние проверки
    public bool isButton = false;
    private Material flashMaterial;

    public static int currentIndex; // Текущий индекс

    private void Start()
    {
        if (flashObject != null)
        {
            flashMaterial = flashObject.GetComponent<Renderer>().material;
            SetFlashAlpha(0); // Изначально вспышка невидима
        }

        if (targetCamera == null)
        {
            Debug.LogWarning("Камера не назначена. Убедитесь, что targetCamera настроена.");
        }

        ApplySettingsByIndex(currentIndex); // Применить настройки для начального индекса
    }

    private void Update()
    {
        // Проверяем, была ли нажата клавиша пробел
        if (Input.GetKeyDown(KeyCode.Space) && !isButton)
        {
            TriggerFlash();
        }
    }

    public void SetIndex(int index)
    {
        currentIndex = Mathf.Clamp(index, 0, flashSettingsByIndex.Length - 1);
        ApplySettingsByIndex(currentIndex);
    }

    private void ApplySettingsByIndex(int index)
    {
        if (index < 0 || index >= flashSettingsByIndex.Length)
        {
            Debug.LogError("Индекс вне диапазона настроек.");
            return;
        }

        Debug.Log($"Применены настройки для индекса {index}");
    }

    public void TriggerFlash()
    {
        if (!isButton)
        {
            StartCoroutine(FlashCoroutine());
        }
    }

    private IEnumerator FlashCoroutine()
    {
        isButton = true;
        cameraFOVController.isEnabled = false;
        cameraMovingScript.isEnabled = false;

        var settings = flashSettingsByIndex[currentIndex];

        // Резкое появление (включение белого экрана)
        SetFlashAlpha(1);
        yield return new WaitForSeconds(flashInstantDuration);

        // Плавное исчезновение
        float timeElapsed = 0;
        while (timeElapsed < flashDuration)
        {
            float alpha = Mathf.Lerp(1, 0, timeElapsed / flashDuration);
            SetFlashAlpha(alpha);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Убедитесь, что объект снова полностью прозрачный
        SetFlashAlpha(0);

        // Выполняем проверку условий
        IsWin = IsWithinBounds(settings) && IsFOVInRange(settings) && IsDOFInRange(settings);

        photoCaptureManager.UpdateAfterFlash(IsWin);

        // Сообщаем о завершении проверки
        Debug.Log(IsWin ? "Win state: True" : "Win state: False");

        isButton = false; // Сброс состояния кнопки
    }

    private bool IsWithinBounds(FlashSettings settings)
    {
        if (flashObject == null) return false;

        Vector3 position = flashObject.transform.position;

        // Проверяем границы по осям X и Y
        return position.x >= settings.screenBoundsX.x && position.x <= settings.screenBoundsX.y &&
               position.y >= settings.screenBoundsY.x && position.y <= settings.screenBoundsY.y;
    }

    private bool IsFOVInRange(FlashSettings settings)
    {
        if (targetCamera == null) return false;

        float fov = targetCamera.fieldOfView;
        return fov >= settings.fovRange.x && fov <= settings.fovRange.y; // Проверяем, входит ли FOV в диапазон
    }

    private bool IsDOFInRange(FlashSettings settings)
    {
        if (targetCamera == null) return false;

        // Получаем компонент PostProcessVolume
        PostProcessVolume volume = targetCamera.GetComponent<PostProcessVolume>();
        if (volume == null || !volume.profile.HasSettings<DepthOfField>())
        {
            Debug.LogWarning("PostProcessVolume или DepthOfField не настроен.");
            return false;
        }

        // Извлекаем настройки Depth of Field
        DepthOfField dof;
        if (volume.profile.TryGetSettings(out dof))
        {
            float focusDistance = dof.focusDistance.value;
            return focusDistance >= settings.dofRange.x && focusDistance <= settings.dofRange.y; // Проверяем, входит ли DOF в диапазон
        }

        return false; // Если настройки не найдены
    }

    private void SetFlashAlpha(float alpha)
    {
        if (flashMaterial != null)
        {
            Color color = flashMaterial.color;
            color.a = alpha;
            flashMaterial.color = color;
        }
    }
}
