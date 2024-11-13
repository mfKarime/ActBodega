using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float speed = 5.0f;           // Velocidad de movimiento
    public float mouseSensitivity = 2.0f; // Sensibilidad del mouse para rotación
    public float minY = -60f;           // Límite inferior de la rotación vertical
    public float maxY = 60f;            // Límite superior de la rotación vertical

    private float rotationY = 0f;
    private CharacterController characterController;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor en el centro
        Cursor.visible = false;                    // Hace invisible el cursor
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Movimiento hacia adelante y atrás con las flechas (↑ y ↓)
        float moveForwardBackward = 0f;
        if (Input.GetKey(KeyCode.UpArrow)) { moveForwardBackward = 1f; }
        if (Input.GetKey(KeyCode.DownArrow)) { moveForwardBackward = -1f; }

        Vector3 move = transform.forward * moveForwardBackward * speed;

        // Rotación con el mouse (tanto horizontal como vertical)
        float rotationX = Input.GetAxis("Mouse X") * mouseSensitivity;
        rotationY -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        rotationY = Mathf.Clamp(rotationY, minY, maxY);

        // Aplicar la rotación de la cámara (solo rotación vertical de la cámara)
        Camera.main.transform.localEulerAngles = new Vector3(rotationY, Camera.main.transform.localEulerAngles.y, 0);
        // Solo rotamos el objeto User en el eje Y (horizontal) para que el movimiento de cámara se mantenga
        transform.Rotate(0, rotationX, 0);

        // Mover al personaje
        characterController.Move(move * Time.deltaTime);
    }
}


