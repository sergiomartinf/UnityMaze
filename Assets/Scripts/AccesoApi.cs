using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;


public class AccesoApi : MonoBehaviour
{
    //Variables para el formulario
    private Button boton;
    private InputField inputUsuario, inputClave;
    private Text textoError;
    
    //Rutas de acceso a la API
    string urlGet = "http://157.230.102.160/produccion/apiunity/get.php";
    string urlPost = "http://157.230.102.160/produccion/apiunity/post.php";
    string urlAcceso = "http://157.230.102.160/produccion/apiunity/acceso.php";
    string urlAccesoDb = "http://157.230.102.160/produccion/apiunity/accesodb.php";

    //Variable pública Usuario (para poder acceder desde otros scripts)
    public Usuario usuario;
    
    //Variables para juego y fin
    public int puntos = 0;    
    public string nombreUsuario;
    
    // Start is called before the first frame update
    void Start()
    {
        //Evito que se destruya el objeto entre escenas (para poder acceder a sus variables públicas después)
        DontDestroyOnLoad(this);
        
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
        
        //Inicializo objeto Usuario
        usuario = new Usuario(inputUsuario.text,inputClave.text);
        
        //Ejemplo comprobar con GET
        //StartCoroutine(ComprobarGet(usuario, urlGet));
        
        //Ejemplo comprobar con POST
        //StartCoroutine(ComprobarPost(usuario, urlPost));

        //Ejemplo probar acceso con POST sin base de datos
        //StartCoroutine(ComprobarAcceso(usuario, urlAcceso));
        
        //Ejemplo probar acceso con POST y con base de datos
        StartCoroutine(ComprobarAcceso(usuario, urlAccesoDb));
    }
    
    IEnumerator ComprobarPost(Usuario usuario, string urlPost)
    {
        WWWForm form = new WWWForm();
        form.AddField("usuario", usuario.usuario);
        form.AddField("clave", usuario.clave);

        using (UnityWebRequest www = UnityWebRequest.Post(urlPost, form))
        {
            yield return www.SendWebRequest();
            string respuesta = (www.isNetworkError || www.isHttpError) ? www.error : www.downloadHandler.text;
            Debug.Log(respuesta);
        }
    }

    IEnumerator ComprobarGet(Usuario usuario, string urlGet)
    {

        string urlData = urlGet + "?usuario=" + usuario.usuario + "&clave=" + usuario.clave;

        using (UnityWebRequest webRequest = UnityWebRequest.Get(urlData))
        {
            yield return webRequest.SendWebRequest();
            string respuesta = (webRequest.isNetworkError) ? webRequest.error : webRequest.downloadHandler.text;
            Debug.Log(respuesta);
        }
    }
    
    IEnumerator ComprobarAcceso(Usuario usuario, string urlAcceso)
    {
       
        WWWForm form = new WWWForm();
        form.AddField("usuario", usuario.usuario);
        form.AddField("clave", usuario.clave);

        using (UnityWebRequest www = UnityWebRequest.Post(urlAcceso, form))
        {
            yield return www.SendWebRequest();
            
            //Compruebo acceso o muestro mensaje de error
            if (www.downloadHandler.text == "1")
            {
                //Cargo la escena de juego
                Debug.Log("Acceso correcto");
                
                //Asigno el nombre de usuario a la variable publica
                nombreUsuario = usuario.usuario;
                SceneManager.LoadScene("Juego");
            }
            else
            {
                //Muestro mensaje de error
                Debug.Log(www.error);
                Debug.Log("Acceso denegado");
                textoError.enabled = true;
            }
        }
    }
}
