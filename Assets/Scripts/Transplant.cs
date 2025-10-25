using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transplant : MonoBehaviour
{
    // El objeto que se quiere activar
    public GameObject objetoADesactivar;

    // El collider que debe activar el objeto (puedes asignarlo desde el Inspector)
    public Collider colliderObjetivo;

    // Método que se llama cuando otro objeto entra en el trigger
    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el collider de la colisión es el que debe desactivar el objeto
        if (other == colliderObjetivo)
        {
            // Activar el objeto
            objetoADesactivar.SetActive(false);
        }
    }
}
