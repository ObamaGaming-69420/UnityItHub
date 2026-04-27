using System.Collections.Generic;
using UnityEngine;

public class CompositeTrigger : BaseTrigger
{
    [Header("Composite Settings")]
    public bool checkAll = false;
    public List<BaseTrigger> childTriggers = new List<BaseTrigger>();

    void Update()
    {
        if (childTriggers.Count == 0) return;

        bool currentStatus = checkAll ? CheckIfAllActive() : CheckIfAnyActive();
        
        // Используем метод из базового класса
        UpdateTriggerStatus(currentStatus);
    }

    private bool CheckIfAllActive()
    {
        foreach (var trigger in childTriggers)
            if (trigger == null || !trigger.isTriggered) return false;
        return true;
    }

    private bool CheckIfAnyActive()
    {
        foreach (var trigger in childTriggers)
            if (trigger != null && trigger.isTriggered) return true;
        return false;
    }
}