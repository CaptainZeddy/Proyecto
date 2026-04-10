using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 2f;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 3f;
    public KeyCode runningKey = KeyCode.LeftShift;

    [HideInInspector] public Rigidbody rigidbody;
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.freezeRotation = true; // Evita que el cuerpo rote al moverse
    }

    void FixedUpdate()
    {
        // Actualiza si se corre
        IsRunning = canRun && Input.GetKey(runningKey);

        // Velocidad objetivo
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();

        // Input de movimiento
        Vector2 targetVelocity = new Vector2(
            Input.GetAxis("Horizontal") * targetMovingSpeed,
            Input.GetAxis("Vertical") * targetMovingSpeed
        );

        // Aplicar movimiento al Rigidbody
        Vector3 move = transform.rotation * new Vector3(targetVelocity.x, rigidbody.linearVelocity.y, targetVelocity.y);
        rigidbody.linearVelocity = move;
    }

    // Propiedad pública para Head Bob
    public Vector3 Velocity => rigidbody.linearVelocity;
}