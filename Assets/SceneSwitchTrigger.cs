using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchTrigger : MonoBehaviour
{
    [Header("Scene Switch Settings")]
    [Tooltip("Имя сцены, на которую нужно переключиться. Оставьте пустым для загрузки следующей сцены по порядку.")]
    public string sceneName = "";

    [Tooltip("Если включено, будет загружена следующая сцена в порядке сборки.")]
    public bool loadNextScene = true;

    [Header("Trigger Settings")]
    [Tooltip("Ссылка на компонент BaseTrigger, который будет использоваться для активации")]
    public BaseTrigger trigger;

    private void OnEnable()
    {
        if (trigger == null)
        {
            trigger = GetComponent<BaseTrigger>();
            if (trigger == null)
            {
                Debug.LogError("SceneSwitchTrigger: Не найден компонент BaseTrigger на этом объекте!");
                return;
            }
        }
    }

    private void Update()
    {
        // Проверяем состояние триггера при каждом кадре
        if (trigger != null && trigger.isTriggered)
        {
            SwitchScene();
        }
    }

    private void SwitchScene()
    {
        if (loadNextScene)
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("SceneSwitchTrigger: Не указана сцена для загрузки!");
        }
        
        // Отключаем компонент после переключения сцены
        this.enabled = false;
    }
}