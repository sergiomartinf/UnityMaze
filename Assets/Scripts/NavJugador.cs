using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavJugador : MonoBehaviour
{
    public float velocidad;
    public bool huir = false;
    private float tiempo;

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
        
        //Rotar
        if (movimiento != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movimiento), 0.15f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Si atraviesa con el coleccionable
        if (other.gameObject.CompareTag("Coleccionable"))
        {
            //Borro el coleccionable
            other.gameObject.SetActive(false);

            //Inicio el contador hacia atrás y pongo a true el booleano
            tiempo = 10;
            huir = true;
        }
    }
}
