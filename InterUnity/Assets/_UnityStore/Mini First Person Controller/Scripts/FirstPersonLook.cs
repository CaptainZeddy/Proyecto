using UnityEngine;
using UnityEngine.Rendering;

public class FirstPersonLook : MonoBehaviour
{
    [Range(0.001f, 0.01f)]
    public float amount = 0.002f; // Cantidad de movimiento del mouse para rotar la cámara
    public float Frecuencia = 0.5f; // Frecuencia de oscilación del movimiento de la cámara
    [Range(10f, 100f)]
    public float Smooth = 10.0f; // Suavizado del movimiento de la cámara

    [SerializeField]
    Transform character;
    public float sensitivity = 2;
    public float smoothing = 1.5f;

    Vector2 velocity;
    Vector2 frameVelocity;
    private Vector3 originalLocalPosition;


    void Reset()
    {
        // Get the character from the FirstPersonMovement in parents.
        character = GetComponentInParent<FirstPersonMovement>().transform;
    }

    void Start()
    {
        // Lock the mouse cursor to the game screen.
        Cursor.lockState = CursorLockMode.Locked;
        originalLocalPosition = transform.localPosition;
    }

    void Update()
    {
        // Get smooth velocity.
        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
        frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
        velocity += frameVelocity;
        velocity.y = Mathf.Clamp(velocity.y, -90, 90);

        // Rotate camera up-down and controller left-right from velocity.
        transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
        character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);

        // Headbob logic
        if (GetComponentInParent<FirstPersonMovement>().Velocity.magnitude > 0.1f)
        {
            // Apply head bob by oscillating the camera's local Y position
            float headBobAmount = Mathf.Sin(Time.time * Frecuencia) * amount;
            transform.localPosition = new Vector3(originalLocalPosition.x, originalLocalPosition.y + headBobAmount, originalLocalPosition.z);
        }
        else
        {
            // Return to original position when not moving
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalLocalPosition, Time.deltaTime * Smooth);
        }
    }

}
