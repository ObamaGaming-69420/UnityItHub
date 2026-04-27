using UnityEngine;

public class MyCustomMover : MonoBehaviour
{
    public KeyCode ForwardKey;
    public KeyCode BackwardKey;
    public KeyCode LeftKey;
    public KeyCode RightKey;

    [Space]
    public float Power;
    public float RotationSpeed;
    public bool UseGravity = true; // Включение/выключение гравитации через инспектор

    private Rigidbody _rigidbody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        if (_rigidbody == null)
        {
            _rigidbody = gameObject.AddComponent<Rigidbody>();
        }
        
        // Настраиваем Rigidbody для движения через физику
        _rigidbody.useGravity = UseGravity;
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(ForwardKey))
        {
            _rigidbody.AddForce(transform.forward * Power, ForceMode.Acceleration);
        }
        else if (Input.GetKey(BackwardKey))
        {
            _rigidbody.AddForce(-transform.forward * Power, ForceMode.Acceleration);
        }

        // Вращение объекта
        if (Input.GetKey(LeftKey))
        {
            transform.Rotate(0, -RotationSpeed * Time.deltaTime, 0);
        }
        else if (Input.GetKey(RightKey))
        {
            transform.Rotate(0, RotationSpeed * Time.deltaTime, 0);
        }
    }
}