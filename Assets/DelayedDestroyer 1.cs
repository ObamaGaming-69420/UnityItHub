using UnityEngine;

public class DelayedDestroyer : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Задержка перед удалением в секундах")]
    public float Delay = 2f;

    [Header("Activation")]
    [Tooltip("Ссылка на триггер. Если не назначен, удаление начнется сразу при старте.")]
    public BaseTrigger ActivationTrigger;
    
    [Header("Finish Trigger")]
    public EventTrigger FinishTrigger;

    private bool _isCountingDown = false;
    private float _timer = 0f;

    void Update()
    {
        // Если триггера нет или он сработал — начинаем отсчет
        if (!_isCountingDown && (ActivationTrigger == null || ActivationTrigger.isTriggered))
        {
            _isCountingDown = true;
            Debug.Log($"Объект {gameObject.name} будет удален через {Delay} сек.");
        }

        if (_isCountingDown)
        {
            _timer += Time.deltaTime;

            if (_timer >= Delay)
            {
                Destroy(gameObject);
                
                if(FinishTrigger != null)
                    FinishTrigger.Activate();
            }
        }
    }
}