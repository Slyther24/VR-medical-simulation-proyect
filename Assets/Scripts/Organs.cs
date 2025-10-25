using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Organs : MonoBehaviour

{
    // El objeto que se quiere activar
    public GameObject objetoAActivar;

    // El collider que debe activar el objeto (puedes asignarlo desde el Inspector)
    public Collider colliderObjetivo;

    // Método que se llama cuando otro objeto entra en el trigger
    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el collider de la colisión es el que debe activar el objeto
        if (other == colliderObjetivo)
        {
            // Activar el objeto
            objetoAActivar.SetActive(true);
        }
    }
}




