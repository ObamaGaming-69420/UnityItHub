using System.Collections.Specialized;
using UnityEngine;

public class MyInstancer : MonoBehaviour
{

    public KeyCode CreateKey;
    public GameObject ObjToCtreate;
    public Vector3 Offset;

    public int ObjectNumToSpawn = int.MaxValue;
    public float Cooldown;

    [Header("Activation")]
    [Tooltip("Ссылка на скрипт триггера. Если не назначено, объект движется сразу.")]
    public BaseTrigger activationTrigger;


    private float _cooldownTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Проверяем, нужно ли двигаться
        // Условие: если триггера нет ИЛИ если он назначен и активирован
        if ((activationTrigger != null && activationTrigger.isTriggered)
            || Input.GetKey(CreateKey))
        {
            InstantiateObjects();
        }
    }

    public void InstantiateObjects()
    {

        _cooldownTimer += Time.deltaTime;

        if (_cooldownTimer < 0)
        {
            return;
        }

        if (ObjectNumToSpawn < 0)
            return;

        _cooldownTimer -= Cooldown;

        var obj = Instantiate(ObjToCtreate);

        var transform = gameObject.GetComponent<Transform>();
        obj.transform.rotation = transform.rotation;
        obj.transform.position = transform.position + Offset;
        ObjectNumToSpawn--;
    }
}
