using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionWithCullingMask : MonoBehaviour
{
    public int sceneBuildIndex; // Индекс сцены для перехода
    public LayerMask targetCullingMask; // Culling mask, который нужно применить в новой сцене
    public string targetCameraName; // Имя камеры, к которой будет применён culling mask
    public KeyCode actionKey = KeyCode.E; // Клавиша для действия
    public int indexPoint;
    private string newTag = "Player2"; // Новый тег, который будет присвоен объекту

    private bool isPlayerInTrigger = false; // Флаг для отслеживания нахождения игрока в зоне триггера

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true; // Устанавливаем флаг в true
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false; // Устанавливаем флаг в false
        }
    }

    void Update()
    {
        // Проверяем, был ли этот индекс уже посещён
        if (SharedState.VisitedIndices.Contains(indexPoint))
        { 
            return; // Если да, выходим из метода
        }

        if (isPlayerInTrigger && Input.GetKeyDown(actionKey))
        {
            // Сохраняем culling mask и имя камеры, затем загружаем сцену
            SceneTransitionManager.TargetCullingMask = targetCullingMask;
            SceneTransitionManager.TargetCameraName = targetCameraName;
            GameEndManager.layerNumber = indexPoint;
            PhotoCaptureManager.index = indexPoint;
            FlashEffect.currentIndex = indexPoint;

            Debug.Log("Before changing tag: " + gameObject.tag);
            gameObject.tag = newTag;
            Debug.Log("After changing tag: " + gameObject.tag);

            // Переход на новую сцену
            SceneManager.LoadScene(sceneBuildIndex);
        }
    }
}
