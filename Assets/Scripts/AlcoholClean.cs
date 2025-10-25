using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlcoholClean : MonoBehaviour
{
    [SerializeField] private ParticleSystem sistemaDeParticulas; // Sistema de partículas a activar
    [SerializeField] private GameObject[] objetosPermitidos; // Arreglo de objetos con los que puede colisionar

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto colisionado está en el arreglo de objetos permitidos
        foreach (GameObject objeto in objetosPermitidos)
        {
            // Si el objeto que colisionó es uno de los permitidos
            if (other.gameObject == objeto)
            {
                // Activa el sistema de partículas en el punto de colisión
                if (sistemaDeParticulas != null)
                {
                    sistemaDeParticulas.transform.position = other.transform.position; // Coloca el sistema de partículas en el punto de colisión
                    sistemaDeParticulas.Play(); // Reproduce las partículas
                }
                break; // Sale del ciclo si encuentra una colisión válida
            }
        }
    }
}




