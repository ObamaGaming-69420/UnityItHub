using UnityEngine;

public class DirectMover : MonoBehaviour
{
    private Rigidbody rb;
    private bool isInitialized = false;
    [Header("Movement Settings")]
    [Tooltip("Направление движения")]
    public Vector3 direction = Vector3.forward;
    
    [Tooltip("Скорость перемещения (единиц в секунду)")]
    public float speed = 5f;
    
    [Tooltip("Использовать локальные координаты для направления")]
    public bool useLocal = true;

    [Header("Activation")]
    [Tooltip("Ссылка на скрипт триггера. Если не назначено, объект движется сразу.")]
    public BaseTrigger activationTrigger;

    [Header("Physics Settings")]
    [Tooltip("Сохранять ли гравитацию для этого объекта")]
    public bool useGravity = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("DirectMover требует компонент Rigidbody на объекте '" + gameObject.name + "'");
            enabled = false;
            return;
        }
        
        rb.useGravity = useGravity;
        rb.isKinematic = false;
        isInitialized = true;
    }

    void FixedUpdate()
    {
        // Проверяем, нужно ли двигаться
        // Условие: если триггера нет ИЛИ если он назначен и активирован
        if (activationTrigger == null || activationTrigger.isTriggered)
        {
            MoveObject();
        }
    }

    private void MoveObject()
    {
        if (!isInitialized) return;
        // Вычисляем смещение за этот кадр
        Vector3 movement = direction * (speed * Time.fixedDeltaTime);

        if (useLocal)
        {
            // Применяем движение в локальном пространстве
            rb.MovePosition(transform.position + transform.TransformDirection(movement));
        }
        else
        {
            // Применяем движение в мировом пространстве
            rb.MovePosition(transform.position + movement);
        }
    }
}