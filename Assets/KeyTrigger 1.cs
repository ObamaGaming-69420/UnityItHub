using UnityEngine;

public class KeyTrigger : BaseTrigger
{
    [Header("Key Settings")]
    public KeyCode activationKey = KeyCode.E;
    
    [Tooltip("Если включено, флаг будет активен только пока кнопка зажата. Если выключено — работает как переключатель (Toggle)")]
    public bool holdToActivate = false;

    void Update()
    {
        if (holdToActivate)
        {
            UpdateTriggerStatus(Input.GetKey(activationKey));
        }
        else
        {
            if (Input.GetKeyDown(activationKey))
            {
                UpdateTriggerStatus(!isTriggered); // Инвертируем состояние (вкл/выкл)
            }
        }
    }
}