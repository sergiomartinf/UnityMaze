using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirPorTiempo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Lo destruyo a los 10 segundos
        Destroy(gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
