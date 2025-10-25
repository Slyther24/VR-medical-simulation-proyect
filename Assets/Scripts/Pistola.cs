using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Pistola : MonoBehaviour
{

    public XRGrabInteractable grabInteract;

    public DisparoRay disparo;
    // Start is called before the first frame update
    void Start()
    {
        grabInteract.activated.AddListener(x => Disparando());
        grabInteract.deactivated.AddListener(x => DejarDisparo());

        
    }

    public void Disparando()
    {
        disparo.Disparar();
    }
    public void DejarDisparo()
    {
        //Espacio para efectos al dejar de disparar
        Debug.Log("Disparandont");
    }

    
}
