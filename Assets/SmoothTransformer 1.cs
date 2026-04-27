using UnityEngine;

public class SmoothTransformer : MonoBehaviour
{
    public enum StopBehavior
    {
        Rewind, // Прогресс откатывается назад к старту
        Freeze, // Прогресс замирает на месте
        Finish  // Прогресс игнорирует отключение триггера и идет до конца
    }

    [Header("Target State")]
    public Vector3 TargetPosition;
    public Vector3 TargetRotation;
    public Vector3 TargetScale = Vector3.one;

    [Header("Settings")]
    public float ProgressSpeed = 2f;
    public bool IsLocal = true;
    public StopBehavior Behavior = StopBehavior.Rewind;

    [Header("Activation")]
    public BaseTrigger ActivationTrigger;
    
    [Header("Finish Trigger")]
    public EventTrigger FinishTrigger;

    private Vector3 _startPosition;
    private Quaternion _startRotation;
    private Vector3 _startScale;
    
    private Vector3 _finalTargetPosition;
    private Quaternion _finalTargetRotation;

    private float _interpolationProgress = 0f;
    private bool _wasActivated = false;

    void Start()
    {
        _startPosition = transform.position;
        _startRotation = transform.rotation;
        _startScale = transform.localScale;

        if (IsLocal)
            _finalTargetPosition = _startPosition + transform.TransformDirection(TargetPosition);
        else
            _finalTargetPosition = TargetPosition;

        _finalTargetRotation = Quaternion.Euler(TargetRotation);
    }

    void Update()
    {
        bool isTriggerActive = ActivationTrigger == null || ActivationTrigger.isTriggered;
        
        // Если хоть раз активировалось (или нет триггера), помечаем для режима Finish
        if (isTriggerActive) _wasActivated = true;

        HandleProgress(isTriggerActive);
        ApplyTransformation();
    }

    private void HandleProgress(bool isTriggerActive)
    {
        if (isTriggerActive)
        {
            // Движемся вперед к 1
            _interpolationProgress += Time.deltaTime * ProgressSpeed;
        }
        else
        {
            // Логика при отключении триггера
            switch (Behavior)
            {
                case StopBehavior.Rewind:
                    _interpolationProgress -= Time.deltaTime * ProgressSpeed;
                    break;

                case StopBehavior.Freeze:
                    // Ничего не делаем, прогресс стоит на месте
                    break;

                case StopBehavior.Finish:
                    if (_wasActivated)
                        _interpolationProgress += Time.deltaTime * ProgressSpeed;
                    break;
            }
        }

        _interpolationProgress = Mathf.Clamp01(_interpolationProgress);
        
        if(Mathf.Approximately(_interpolationProgress, 1f) && FinishTrigger != null)
            FinishTrigger.Activate();
    }

    private void ApplyTransformation()
    {
        transform.position = Vector3.Lerp(_startPosition, _finalTargetPosition, _interpolationProgress);
        transform.rotation = Quaternion.Lerp(_startRotation, _finalTargetRotation, _interpolationProgress);
        transform.localScale = Vector3.Lerp(_startScale, TargetScale, _interpolationProgress);
    }
}
