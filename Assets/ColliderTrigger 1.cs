using System.Collections.Generic;
using UnityEngine;

public class ColliderTrigger : BaseTrigger
{
    [Header("Settings")]
    [Tooltip("Список тегов, на которые будет реагировать триггер")]
    public List<string> targetTags = new List<string> { "Player" };


    // Срабатывает, когда другой объект входит в зону триггера
    private void OnTriggerEnter(Collider other)
    {
        if (targetTags.Contains(other.tag))
        {
            UpdateTriggerStatus(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (targetTags.Contains(other.tag))
        {
            UpdateTriggerStatus(false);
        }
    }
}