using UnityEngine;

public class MyInstancer1 : MonoBehaviour
{
    public KeyCode CreateKey;
    public GameObject ObjToCtreate;
    public Vector3 Offset;

    public int ObjectNumToSpawn = int.MaxValue;
    public float Cooldown;

    [Header("Activation")]
    public BaseTrigger activationTrigger;

    [Header("Audio")]
    public AudioClip SpawnSound; // Сюда перетащи звук сыра
    private AudioSource _audioSource;

    private float _cooldownTimer;

    void Start()
    {
        // Добавляем AudioSource автоматически, если его нет
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if ((activationTrigger != null && activationTrigger.isTriggered)
            || Input.GetKey(CreateKey))
        {
            InstantiateObjects();
        }
    }

    public void InstantiateObjects()
    {
        _cooldownTimer += Time.deltaTime;

        if (_cooldownTimer < Cooldown) // Исправил логику таймера для точности
        {
            return;
        }

        if (ObjectNumToSpawn <= 0)
            return;

        _cooldownTimer = 0; // Сбрасываем таймер

        var obj = Instantiate(ObjToCtreate);

        var myTransform = transform; // Оптимизация обращения к transform
        obj.transform.rotation = myTransform.rotation;
        obj.transform.position = myTransform.position + Offset;

        // Воспроизведение звука
        if (SpawnSound != null && _audioSource != null)
        {
            _audioSource.PlayOneShot(SpawnSound);
        }

        ObjectNumToSpawn--;
    }
}