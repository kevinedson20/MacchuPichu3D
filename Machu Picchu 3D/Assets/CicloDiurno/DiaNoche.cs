using UnityEngine;

public class DiaNoche : MonoBehaviour
{
    [Range(0.0f, 24f)] public float Hora = 12f;
    public Transform luzDelSol;          // Directional Light
    public Transform solVisual;          // Esfera u objeto 3D del Sol
    public float radioSol = 50f;         // Distancia del sol visual desde el centro
    public float velocidadCiclo = 0.1f;

    private void Update()
    {
        Hora += Time.deltaTime * velocidadCiclo;
        if (Hora >= 24f) Hora = 0f;

        RotarSol();
    }

    void RotarSol()
    {
        // Rotación de la luz direccional
        float anguloX = 15f * Hora;
        luzDelSol.localEulerAngles = new Vector3(anguloX, 0, 0);

        // Movimiento visual del "sol"
        if (solVisual != null)
        {
            // Convertir la hora a ángulo en radianes
            float angulo = (Hora / 24f) * Mathf.PI * 2f;
            Vector3 posicion = new Vector3(
                0,
                Mathf.Sin(angulo),
                Mathf.Cos(angulo)
            ) * radioSol;

            solVisual.position = posicion;
        }
    }
}
