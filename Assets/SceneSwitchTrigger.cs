using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

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
        // Запускаем корутину загрузки
        StartCoroutine(LoadSceneWithDelay());

        // Сразу отключаем скрипт, чтобы Update не вызывал загрузку много раз
        this.enabled = false;
    }

    private IEnumerator LoadSceneWithDelay()
    {
        // Ждем 2 секунды (можешь заменить на свою переменную)
        yield return new WaitForSeconds(5f);

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