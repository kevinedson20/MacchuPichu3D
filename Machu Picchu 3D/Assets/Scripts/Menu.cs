using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    // Puedes asignar los botones desde el Inspector

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Entrar()
    {
        Debug.Log("Entrando al juego...");
        // Cargar escena del juego (aseg�rate de agregarla en el Build Settings)
        SceneManager.LoadScene("Machu Picchu"); // Reemplaza con el nombre real
    }

    public void AbrirConfiguraciones()
    {
        Debug.Log("Abriendo configuraciones...");
        // Aqu� puedes mostrar un panel de configuraci�n o cargar una escena
        // Ejemplo: SceneManager.LoadScene("Configuraciones");
    }

    public void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit(); // Esto funciona en el build, no en el editor
    }
}
