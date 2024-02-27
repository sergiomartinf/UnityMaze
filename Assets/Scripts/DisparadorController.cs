using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparadorController : MonoBehaviour
{
    //Declaro la variable para el prefab de disparo
    [SerializeField] private GameObject disparo;
    
    //Declaro la variable de tipo float velocidadDisparo para la velocidad con la que sale el disparo
    [SerializeField] private float velocidadDisparo = 10f;
    
    //Declaro la variable de tipo float tiempoEntreDisparos para la velocidad con la que puedo generar disparos
    [SerializeField] 
    private float tiempoEntreDisparos = 0.25f; //4 por segundo

    //Tiempo que tiene que transcurrir hasta el próximo disparo
    private float proximoDisparo;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > proximoDisparo){

            //Incremento el valor de proximo disparo
            proximoDisparo = Time.time + tiempoEntreDisparos;

            //Instancio un nuevo disparo en esa posición
            GameObject disparoActual = Instantiate(disparo, transform.position, transform.rotation) as GameObject;
            
            //Lanzo el disparo en la dirección en la que apunta el disparador y con su velocidad
            disparoActual.GetComponent<Rigidbody>().velocity = transform.forward * velocidadDisparo;

        }
    }
}
