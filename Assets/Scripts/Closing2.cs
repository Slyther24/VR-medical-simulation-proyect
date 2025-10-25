using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closing2 : MonoBehaviour
{
    public GameObject object1;       // Primer objeto que puede encenderse o apagarse
    public GameObject object2;       // Segundo objeto que puede encenderse o apagarse
    public GameObject object3;       // Tercer objeto que puede encenderse o apagarse
    public GameObject object4;       // Cuarto objeto que puede encenderse o apagarse
    public Canvas canvas1;           // Canvas que se desactivar� despu�s de alcanzar el l�mite
    public Canvas canvas2;           // Canvas que se activar� despu�s de alcanzar el l�mite
    public int maxSwitches = 4;      // N�mero m�ximo de veces que se permite alternar

    private int currentSwitches = 0; // Contador de alternancias
    private GameObject[] objects;    // Arreglo para los objetos a alternar
    private int currentIndex = 0;    // �ndice del objeto actualmente activo

    void Start()
    {
        // Inicializar el estado de los objetos y canvas
        objects = new GameObject[] { object1, object2, object3, object4 };

        // Configurar todos los objetos: uno activo y el resto apagado
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(i == 0);  // El primer objeto se activa, los dem�s se desactivan
        }

        canvas1.gameObject.SetActive(true); // Canvas1 comienza activo
        canvas2.gameObject.SetActive(false); // Canvas2 comienza apagado

        Debug.Log("Canvas1 inicial activo: " + canvas1.gameObject.activeSelf);
        Debug.Log("Canvas2 inicial activo: " + canvas2.gameObject.activeSelf);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si se ha alcanzado el l�mite, no hacer nada
        if (currentSwitches >= maxSwitches)
        {
            Debug.Log("Switch limit reached, no action taken.");
            return;
        }

        // Detecta si el objeto que entra en el Trigger es uno de los objetos a alternar
        for (int i = 0; i < objects.Length; i++)
        {
            if (other.gameObject == objects[i])
            {
                SwitchObjects();
                break; // Solo alterna una vez por objeto en el trigger
            }
        }
    }

    private void SwitchObjects()
    {
        // Apagar el objeto actual
        objects[currentIndex].SetActive(false);

        // Avanzar al siguiente objeto y activarlo
        currentIndex = (currentIndex + 1) % objects.Length; // Esto asegura que el �ndice sea c�clico
        objects[currentIndex].SetActive(true);

        // Incrementar el contador de alternancias
        currentSwitches++;
        Debug.Log("Switch count: " + currentSwitches);

        // Verificar si se ha alcanzado el l�mite de alternancias
        if (currentSwitches >= maxSwitches)
        {
            Debug.Log("Switch limit reached! Changing canvases.");

            // Desactivar el Canvas1 y activar el Canvas2
            canvas1.gameObject.SetActive(false);
            canvas2.gameObject.SetActive(true);

            Debug.Log("Canvas1 activo: " + canvas1.gameObject.activeSelf);
            Debug.Log("Canvas2 activo: " + canvas2.gameObject.activeSelf);
        }
    }

}


