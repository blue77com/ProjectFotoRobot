using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using System.Collections;

public class FlashEffect : MonoBehaviour
{
    public PhotoCaptureManager photoCaptureManager; // ������ �� PhotoCaptureManager
    public GameObject flashObject; // ������ �� ������ ������� (Quad ��� Sprite)
    public float flashDuration = 1.0f; // ������������ ������� �������� ������������
    public float flashInstantDuration = 0.5f; // ������������ ������� ���������
    [System.Serializable]
    public class FlashSettings
    {
        public Vector2 screenBoundsX; // ������� �� ��� X
        public Vector2 screenBoundsY; // ������� �� ��� Y
        public Vector2 fovRange; // �������� ����������� �������� Field of View
        public Vector2 dofRange; // �������� ����������� �������� Depth of Field
    }

    public FlashSettings[] flashSettingsByIndex; // ��������� ��� ������� �������
    public Camera targetCamera; // ������ ��� �������� FOV � DOF

    public InfiniteScrollFrameAnimation ScrollFrameAnimation;
    public CameraFOVController cameraFOVController;
    public CameraMoving cameraMovingScript;

    public bool IsWin { get; private set; } // ��������� ��������
    public bool isButton = false;
    private Material flashMaterial;

    public static int currentIndex; // ������� ������

    private void Start()
    {
        if (flashObject != null)
        {
            flashMaterial = flashObject.GetComponent<Renderer>().material;
            SetFlashAlpha(0); // ���������� ������� ��������
        }

        if (targetCamera == null)
        {
            Debug.LogWarning("������ �� ���������. ���������, ��� targetCamera ���������.");
        }

        ApplySettingsByIndex(currentIndex); // ��������� ��������� ��� ���������� �������
    }

    private void Update()
    {
        // ���������, ���� �� ������ ������� ������
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
            Debug.LogError("������ ��� ��������� ��������.");
            return;
        }

        Debug.Log($"��������� ��������� ��� ������� {index}");
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

        // ������ ��������� (��������� ������ ������)
        SetFlashAlpha(1);
        yield return new WaitForSeconds(flashInstantDuration);

        // ������� ������������
        float timeElapsed = 0;
        while (timeElapsed < flashDuration)
        {
            float alpha = Mathf.Lerp(1, 0, timeElapsed / flashDuration);
            SetFlashAlpha(alpha);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // ���������, ��� ������ ����� ��������� ����������
        SetFlashAlpha(0);

        // ��������� �������� �������
        IsWin = IsWithinBounds(settings) && IsFOVInRange(settings) && IsDOFInRange(settings);

        photoCaptureManager.UpdateAfterFlash(IsWin);

        // �������� � ���������� ��������
        Debug.Log(IsWin ? "Win state: True" : "Win state: False");

        isButton = false; // ����� ��������� ������
    }

    private bool IsWithinBounds(FlashSettings settings)
    {
        if (flashObject == null) return false;

        Vector3 position = flashObject.transform.position;

        // ��������� ������� �� ���� X � Y
        return position.x >= settings.screenBoundsX.x && position.x <= settings.screenBoundsX.y &&
               position.y >= settings.screenBoundsY.x && position.y <= settings.screenBoundsY.y;
    }

    private bool IsFOVInRange(FlashSettings settings)
    {
        if (targetCamera == null) return false;

        float fov = targetCamera.fieldOfView;
        return fov >= settings.fovRange.x && fov <= settings.fovRange.y; // ���������, ������ �� FOV � ��������
    }

    private bool IsDOFInRange(FlashSettings settings)
    {
        if (targetCamera == null) return false;

        // �������� ��������� PostProcessVolume
        PostProcessVolume volume = targetCamera.GetComponent<PostProcessVolume>();
        if (volume == null || !volume.profile.HasSettings<DepthOfField>())
        {
            Debug.LogWarning("PostProcessVolume ��� DepthOfField �� ��������.");
            return false;
        }

        // ��������� ��������� Depth of Field
        DepthOfField dof;
        if (volume.profile.TryGetSettings(out dof))
        {
            float focusDistance = dof.focusDistance.value;
            return focusDistance >= settings.dofRange.x && focusDistance <= settings.dofRange.y; // ���������, ������ �� DOF � ��������
        }

        return false; // ���� ��������� �� �������
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
