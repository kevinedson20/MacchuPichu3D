using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class Movimiento : MonoBehaviour
{
    public float velocidad = 5f;
    public float fuerzaSalto = 7f;
    public LayerMask capaSuelo;  // Para detectar el suelo
    public Transform comprobadorSuelo;  // Un objeto en los pies del personaje
    public float radioComprobacion = 0.2f;

    private Rigidbody rb;
    private Transform camaraTransform;
    private Animator animator;

    private bool estaEnSuelo = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camaraTransform = Camera.main.transform;
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        // Dirección según la cámara
        Vector3 direccionDeseada = camaraTransform.forward * y + camaraTransform.right * x;
        direccionDeseada.y = 0f;
        direccionDeseada.Normalize();

        // Movimiento físico
        Vector3 nuevaPosicion = rb.position + direccionDeseada * velocidad * Time.fixedDeltaTime;
        rb.MovePosition(nuevaPosicion);

        // Input relativo al personaje para el Blend Tree
        Vector3 inputLocal = transform.InverseTransformDirection(direccionDeseada);
        animator.SetFloat("VelX", inputLocal.x);
        animator.SetFloat("VelY", inputLocal.z);

        // Rotación del personaje
        if (direccionDeseada.magnitude > 0.1f)
        {
            Quaternion rotacionObjetivo = Quaternion.LookRotation(direccionDeseada);
            rb.rotation = Quaternion.Slerp(rb.rotation, rotacionObjetivo, 10f * Time.fixedDeltaTime);
        }

        // Comprobar si está en el suelo
        estaEnSuelo = Physics.CheckSphere(comprobadorSuelo.position, radioComprobacion, capaSuelo);

        // Salto
         if (Input.GetButtonDown("Jump") && estaEnSuelo)
        {
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        }
    }
}
