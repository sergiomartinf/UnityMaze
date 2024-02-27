using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class Jugador : MonoBehaviour
{
    public float velocidad;
    public bool huir = false;
    private float tiempo;
    private AccesoApi accesoApi;
    [SerializeField] private Text textoUsuario, textoPuntos;

    private void Start()
    {
        //Busco el script de AccesoApi para acceder a las variables
        accesoApi = FindObjectOfType<AccesoApi>();
        
        //Valores iniciales de las cajas de texto
        textoUsuario.text = "Usuario: " + accesoApi.nombreUsuario;
        textoPuntos.text = "Puntos: " + accesoApi.puntos;
    }

    void FixedUpdate () {

        //Capturo el movimiento en los ejes
        float movimientoH = Input.GetAxis("Horizontal");
        float movimientoV = Input.GetAxis("Vertical");

        //Genero el vector de movimiento
        Vector3 movimiento = new Vector3(movimientoH, 0, movimientoV);

        //Muevo el jugador
        transform.position += movimiento * velocidad;

        //Si los enemigos están huyendo y no se ha acabado el tiempo, decremento el tiempo
        if (huir && tiempo > 0)
        {
            tiempo -= Time.deltaTime;
            //Lo muestro en consola
            Debug.Log(tiempo);
        }
        else
        {
            huir = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Si atraviesa con el coleccionable
        if (other.gameObject.CompareTag("Coleccionable"))
        {
            //Incremento 50 pts
            actualizarPuntos(50);
            
            //Borro el coleccionable
            Destroy(other.gameObject);

            //Inicio el contador hacia atrás y pongo a true el booleano
            tiempo = 10;
            huir = true;
        }
        
        if (other.gameObject.CompareTag("Enemigo"))        
        {            
            //Borro el coleccionable
            if (!huir)
            {
                gameObject.SetActive(false);
            }
            else
            {
                other.gameObject.SetActive(false);
            }        
        }
    }

    public void actualizarPuntos(int cantidad)
    {
        //Incremento puntos y actualizo el contador
        accesoApi.puntos += cantidad;
        textoPuntos.text = "Puntos: " + accesoApi.puntos;
    }
}
