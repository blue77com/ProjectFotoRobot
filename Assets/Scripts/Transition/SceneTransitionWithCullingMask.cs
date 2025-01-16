using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionWithCullingMask : MonoBehaviour
{
    public int sceneBuildIndex; // ������ ����� ��� ��������
    public LayerMask targetCullingMask; // Culling mask, ������� ����� ��������� � ����� �����
    public string targetCameraName; // ��� ������, � ������� ����� ������� culling mask
    public KeyCode actionKey = KeyCode.E; // ������� ��� ��������
    public int indexPoint;
    private string newTag = "Player2"; // ����� ���, ������� ����� �������� �������

    private bool isPlayerInTrigger = false; // ���� ��� ������������ ���������� ������ � ���� ��������

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true; // ������������� ���� � true
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false; // ������������� ���� � false
        }
    }

    void Update()
    {
        // ���������, ��� �� ���� ������ ��� �������
        if (SharedState.VisitedIndices.Contains(indexPoint))
        { 
            return; // ���� ��, ������� �� ������
        }

        if (isPlayerInTrigger && Input.GetKeyDown(actionKey))
        {
            // ��������� culling mask � ��� ������, ����� ��������� �����
            SceneTransitionManager.TargetCullingMask = targetCullingMask;
            SceneTransitionManager.TargetCameraName = targetCameraName;
            GameEndManager.layerNumber = indexPoint;
            PhotoCaptureManager.index = indexPoint;
            FlashEffect.currentIndex = indexPoint;

            Debug.Log("Before changing tag: " + gameObject.tag);
            gameObject.tag = newTag;
            Debug.Log("After changing tag: " + gameObject.tag);

            // ������� �� ����� �����
            SceneManager.LoadScene(sceneBuildIndex);
        }
    }
}
