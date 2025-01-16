using UnityEngine;

public class SceneTagChanger : MonoBehaviour
{
    public string newTag = "Player2"; // Новый тег, который будет присвоен объектам

    void Start()
    {
        // Перебор всех объектов с тегом "Player", чтобы изменить их теги
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Props"))
        {
            // Если объект был посещён (индекс есть в списке)
            if (SharedState.VisitedIndices.Contains(obj.GetComponent<SceneTransitionWithCullingMask>().indexPoint))
            {
                // Изменяем тег объекта
                obj.tag = newTag;
            }
        }
    }
}
