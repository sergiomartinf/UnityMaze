using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class VolcadoApi : MonoBehaviour
{
    //Variables para conectar con acceso y para el volcado
    private AccesoApi accesoApi;
    private string nombreJugador;
    private int puntos;
    private GameManager gameManager;
    private Partida partida;
    
    //Textos
    public Text textoUsuario, textoPuntos, textoFecha, textoPartidas;
    
    //Rutas de acceso a la API
    string urlAccesoDb = "http://157.230.102.160/produccion/apiunity/accesodb.php";
    string urlPartida = "http://157.230.102.160/produccion/apiunity/partida.php";
    string urlPartidas = "http://157.230.102.160/produccion/apiunity/partidas.php";

    // Start is called before the first frame update
    void Start()
    {
        //Paro la escala de tiempo para evitar que se creen más enemigos en el GameManager
        Time.timeScale = 0;
        
        //Cojo el usuario de AccesoApi y lo muestro en la caja de texto
        nombreJugador = FindObjectOfType<AccesoApi>().nombreUsuario;
        textoUsuario.text = "Jugador: " + nombreJugador;
        
        //Cojo los puntos del AccesoApi y los muestro en la caja de texto
        puntos = FindObjectOfType<AccesoApi>().puntos;
        textoPuntos.text = "Puntos: " + puntos;

        //Genero la fecha y la muestro en la caja de texto
        string fechaDb = System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
        string fecha = System.DateTime.Now.ToString("dd/MM/yy hh:mm");
        textoFecha.text = "Fecha: " + fecha;

        //Vuelco los datos a la base de datos
        partida = new Partida(nombreJugador, puntos, fechaDb);
        StartCoroutine(EscribirPartida());

        //Recupero las 5 últimas partidas y las muestro en la caja de texto
        StartCoroutine(MostrarPartidas());
    }
    
    IEnumerator EscribirPartida()
    {
        WWWForm form = new WWWForm();
        form.AddField("jugador", partida.jugador);
        form.AddField("puntos", partida.puntos);
        form.AddField("fecha", partida.fecha);

        using (UnityWebRequest www = UnityWebRequest.Post(urlPartida, form))
        {
            yield return www.SendWebRequest();
            
            //Compruebo acceso o muestro mensaje de error
            if (www.downloadHandler.text == "1")
            {
                //Muestro mensaje de OK
                Debug.Log("Volcado correcto");
            }
            else
            {
                //Muestro mensaje de error
                Debug.Log(www.error);
                Debug.Log("Error de conexión");
            }
        }
    }
    
    IEnumerator MostrarPartidas()
    {

        using (UnityWebRequest webRequest = UnityWebRequest.Get(urlPartidas))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                string jsonResponse = webRequest.downloadHandler.text;
                Partida[] partidas = JsonHelper.getJsonArray<Partida> (jsonResponse);
                string texto = "Mejores 5 partidas:\n";

                for (int i = 0; i < partidas.Length; i++)
                {
                    texto += partidas[i].fecha + " | " + partidas[i].puntos + " | " + partidas[i].jugador + "\n";
                }
                
                //Escribo en la caja de texto de las partidas
                textoPartidas.text = texto;
            }
        }
   
    
    }
}