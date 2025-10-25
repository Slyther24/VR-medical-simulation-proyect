using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeshDeform : MonoBehaviour

{
    public Canvas imageToShow;        // Imagen que se mostrará al colisionar
    public GameObject targetObject;  // El objeto específico que activará la acción

    private void Start()
    {
        // Asegura que la imagen esté oculta al inicio
        if (imageToShow != null)
        {
            imageToShow.gameObject.SetActive(false);
        }
    }

    // Método que se ejecuta cuando otro objeto entra en el trigger
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra en el trigger es el asignado
        if (other.gameObject == targetObject)
        {
            // Muestra la imagen si está configurada
            if (imageToShow != null)
            {
                imageToShow.gameObject.SetActive(true);
            }

            // Desactiva el objeto que contiene este script
            gameObject.SetActive(false);
        }
    }
}



