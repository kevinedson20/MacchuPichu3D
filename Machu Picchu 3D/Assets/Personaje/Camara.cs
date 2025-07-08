using UnityEngine;

public class Camara : MonoBehaviour
{
    public Transform objetivo;                    // Personaje
    public Vector3 offset = new Vector3(0f, 2f, -5f);
    public float sensibilidadMouse = 3f;
    public float suavizadoRotacion = 10f;
    public float distanciaMin = 2f;
    public float distanciaMax = 10f;
    public float sensibilidadZoom = 2f;

    private float rotacionY;                      // Horizontal (izquierda/derecha)
    private float rotacionX;                      // Vertical (arriba/abajo)
    private float distanciaActual;                // Zoom din�mico

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rotacionY = transform.eulerAngles.y;
        rotacionX = 15f;
        distanciaActual = offset.magnitude; // Distancia inicial de la c�mara al objetivo
    }

    void LateUpdate()
    {
        if (objetivo == null) return;

        // Movimiento del mouse
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadMouse;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadMouse;

        rotacionY += mouseX;
        rotacionX -= mouseY;
        rotacionX = Mathf.Clamp(rotacionX, -20f, 60f); // Limita la c�mara para no pasar por debajo del personaje

        // Zoom con rueda del mouse
        float zoom = Input.GetAxis("Mouse ScrollWheel") * sensibilidadZoom;
        distanciaActual -= zoom;
        distanciaActual = Mathf.Clamp(distanciaActual, distanciaMin, distanciaMax);

        // Calcular nueva posici�n de c�mara
        Quaternion rotacion = Quaternion.Euler(rotacionX, rotacionY, 0f);
        Vector3 nuevaPosicion = objetivo.position - rotacion * Vector3.forward * distanciaActual;

        transform.position = Vector3.Lerp(transform.position, nuevaPosicion, suavizadoRotacion * Time.deltaTime);
        transform.LookAt(objetivo.position + Vector3.up * 1.5f);

        // Hacer que el personaje mire en la misma direcci�n horizontal que la c�mara
        Vector3 direccionHorizontal = transform.forward;
        direccionHorizontal.y = 0;
        objetivo.forward = Vector3.Lerp(objetivo.forward, direccionHorizontal, Time.deltaTime * 10f);
    }
}
