using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Acceso : MonoBehaviour
{
    //Variables para el formulario
    private Button boton;
    private InputField inputUsuario, inputClave;
    private Text textoError;
    
    // Start is called before the first frame update
    void Start()
    {
        //Capturo el componente Button del objeto llamado Botón
        boton = GameObject.Find("Boton").GetComponent<Button>();
        
        //Acción onclick
        boton.onClick.AddListener(() => Acceder());
        
        //Capturo la caja de texto de error y la desactivo
        textoError = GameObject.Find("Error").GetComponent<Text>();
        textoError.enabled = false;

    }

    void Acceder()
    {
        //Capturo los inputField del formulario
        inputUsuario = GameObject.Find("Usuario").GetComponent<InputField>();
        inputClave = GameObject.Find("Clave").GetComponent<InputField>();
        
        //Compruebo acceso o muestro mensaje de error
        if (inputUsuario.text == "jairo" && inputClave.text == "12345678")
        {
            //Cargo la escena de juego
            SceneManager.LoadScene("Juego");
        }
        else
        {
            //Muestro mensaje de error
            textoError.enabled = true;
        }
        
    }
}
