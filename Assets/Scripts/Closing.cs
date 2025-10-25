using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closing : MonoBehaviour
{
    public GameObject object1;       // Primer objeto que puede encenderse o apagarse
    public GameObject object2;       // Segundo objeto que puede encenderse o apagarse
    public Canvas canvas1;           // Canvas que se desactivará después de alcanzar el límite
    public Canvas canvas2;           // Canvas que se activará después de alcanzar el límite
    public int maxSwitches = 5;      // Número máximo de veces que se permite alternar

    private int currentSwitches = 0; // Contador de alternancias
    private bool isObject1Active = true; // Controla cuál de los objetos está activo

    void Start()
    {
        // Inicializar el estado de los objetos y canvas
        object1.SetActive(true);  // Object1 inicia encendido
        object2.SetActive(false); // Object2 inicia apagado
        canvas1.gameObject.SetActive(true); // Canvas1 comienza activo
        canvas2.gameObject.SetActive(false); // Canvas2 comienza apagado

        Debug.Log("Canvas1 inicial activo: " + canvas1.gameObject.activeSelf);
        Debug.Log("Canvas2 inicial activo: " + canvas2.gameObject.activeSelf);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si se ha alcanzado el límite, no hacer nada
        if (currentSwitches >= maxSwitches)
        {
            Debug.Log("Switch limit reached, no action taken.");
            return;
        }

        // Detecta si el objeto que entra en el Trigger es Object1 o Object2
        if (other.gameObject == object1 && isObject1Active)
        {
            SwitchObjects(object1, object2);
        }
        else if (other.gameObject == object2 && !isObject1Active)
        {
            SwitchObjects(object2, object1);
        }
    }

    private void SwitchObjects(GameObject toDeactivate, GameObject toActivate)
    {
        // Cambiar el estado de los objetos
        toDeactivate.SetActive(false);
        toActivate.SetActive(true);
        isObject1Active = !isObject1Active;

        // Incrementar el contador y verificar si se alcanzó el límite
        currentSwitches++;
        Debug.Log("Switch count: " + currentSwitches);

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
















