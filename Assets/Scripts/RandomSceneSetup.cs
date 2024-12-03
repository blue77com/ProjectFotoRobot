using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class RandomSceneSetup : MonoBehaviour
{
    public GameObject targetObject; // ������ ��� ��������� �������
    public Vector2 positionRangeX; // �������� �� ��� X
    public Vector2 positionRangeY; // �������� �� ��� Y

    public Camera targetCamera; // ������ ��� ��������� FOV
    public Vector2 fovRange; // �������� �������� FOV

    public PostProcessVolume postProcessVolume; // ������������� ��� DOF
    public Vector2 dofRange; // �������� �������� DOF

    private void Start()
    {
        // ��������� ���������� ��������� �������
        if (targetObject != null)
        {
            float randomX = Random.Range(positionRangeX.x, positionRangeX.y);
            float randomY = Random.Range(positionRangeY.x, positionRangeY.y);
            Vector3 newPosition = new Vector3(randomX, randomY, targetObject.transform.position.z);
            targetObject.transform.position = newPosition;

            Debug.Log($"Object Position: {newPosition}");
        }

        // ��������� ���������� �������� FOV ��� ������
        if (targetCamera != null)
        {
            float randomFOV = Random.Range(fovRange.x, fovRange.y);
            targetCamera.fieldOfView = randomFOV;

            Debug.Log($"Camera FOV: {randomFOV}");
        }

        // ��������� ���������� �������� DOF
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
