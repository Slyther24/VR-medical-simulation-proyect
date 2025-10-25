using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeshDeform : MonoBehaviour

{
    public Canvas imageToShow;        // Imagen que se mostrar� al colisionar
    public GameObject targetObject;  // El objeto espec�fico que activar� la acci�n

    private void Start()
    {
        // Asegura que la imagen est� oculta al inicio
        if (imageToShow != null)
        {
            imageToShow.gameObject.SetActive(false);
        }
    }

    // M�todo que se ejecuta cuando otro objeto entra en el trigger
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra en el trigger es el asignado
        if (other.gameObject == targetObject)
        {
            // Muestra la imagen si est� configurada
            if (imageToShow != null)
            {
                imageToShow.gameObject.SetActive(true);
            }

            // Desactiva el objeto que contiene este script
            gameObject.SetActive(false);
        }
    }
}



