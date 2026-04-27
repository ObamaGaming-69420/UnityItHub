using UnityEngine;

public class EventTrigger : BaseTrigger
{
    [Header("Event Settings")]
    [Tooltip("Автоматически сбрасывать триггер в false через указанное время. Если 0 — не сбрасывать.")]
    public float autoResetDelay = 0f;

    /// <summary>
    /// Публичный метод, который можно вызвать из других скриптов или UnityEvents
    /// </summary>
    public void Activate()
    {
        UpdateTriggerStatus(true);

        if (autoResetDelay > 0 && !stayActiveAfterTrigger)
        {
            Invoke(nameof(Deactivate), autoResetDelay);
        }
    }

    /// <summary>
    /// Метод для ручного или автоматического выключения
    /// </summary>
    public void Deactivate()
    {
        // Если stayActiveAfterTrigger включен, UpdateTriggerStatus проигнорирует false
        UpdateTriggerStatus(false);
    }
}