using UnityEngine;

public abstract class BaseTrigger : MonoBehaviour
{
    [Header("Base Settings")]
    [Tooltip("Если включено, триггер останется активным навсегда после первого срабатывания")]
    public bool stayActiveAfterTrigger = false;

    public bool isTriggered = false;

    // Вспомогательный метод для наследников, чтобы они не затирали логику "липкости"
    protected void UpdateTriggerStatus(bool newState)
    {
        if (isTriggered && stayActiveAfterTrigger) 
            return;

        isTriggered = newState;
    }
}