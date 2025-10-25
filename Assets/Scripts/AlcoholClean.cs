using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlcoholClean : MonoBehaviour
{
    [SerializeField] private ParticleSystem sistemaDeParticulas; // Sistema de part�culas a activar
    [SerializeField] private GameObject[] objetosPermitidos; // Arreglo de objetos con los que puede colisionar

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto colisionado est� en el arreglo de objetos permitidos
        foreach (GameObject objeto in objetosPermitidos)
        {
            // Si el objeto que colision� es uno de los permitidos
            if (other.gameObject == objeto)
            {
                // Activa el sistema de part�culas en el punto de colisi�n
                if (sistemaDeParticulas != null)
                {
                    sistemaDeParticulas.transform.position = other.transform.position; // Coloca el sistema de part�culas en el punto de colisi�n
                    sistemaDeParticulas.Play(); // Reproduce las part�culas
                }
                break; // Sale del ciclo si encuentra una colisi�n v�lida
            }
        }
    }
}




