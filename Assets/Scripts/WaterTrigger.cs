using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;


public class WaterTrigger : MonoBehaviour
{
    public Canvas canvas; // Canvas que mostrar� el texto
    public TextMeshPro canvasText; // Texto del Canvas
    public string initialText = "Presiona E para activar"; // Texto inicial
    public string secondText = "Presiona E para desactivar"; // Texto al cambiar
    public ParticleSystem particleSystem1; // Sistema de part�culas a controlar
    public string playerTag = "Player"; // Tag del jugador
    public KeyCode actionKey = KeyCode.E; // Tecla de acci�n (por defecto 'E')
    public string vrButton2 = "Fire1"; // Bot�n de VR para activar la acci�n

    private bool isPlayerNearby = false; // Verifica si el jugador est� cerca
    private bool isParticlesActive = false; // Estado del sistema de part�culas

    private void Start()
    {
        // Oculta el Canvas al inicio y configura el texto inicial
        if (canvas != null)
        {
            canvas.gameObject.SetActive(false);
        }

        if (canvasText != null)
        {
            canvasText.text = initialText;
        }

        // Asegura que las part�culas est�n desactivadas al inicio
        if (particleSystem1 != null)
        {
            particleSystem1.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Activa el Canvas cuando el jugador se acerca
        if (other.CompareTag(playerTag))
        {
            isPlayerNearby = true;
            if (canvas != null)
            {
                canvas.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Oculta el Canvas cuando el jugador se aleja
        if (other.CompareTag(playerTag))
        {
            isPlayerNearby = false;
            if (canvas != null)
            {
                canvas.gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        // Verifica si el jugador est� cerca y presiona la tecla de acci�n o el bot�n VR
        if (isPlayerNearby && (Input.GetKeyDown(actionKey) || Input.GetButtonDown(vrButton2)))
        {
            isParticlesActive = !isParticlesActive; // Alterna el estado de las part�culas

            if (particleSystem1 != null)
            {
                if (isParticlesActive)
                {
                    particleSystem1.Play();
                }
                else
                {
                    particleSystem1.Stop();
                }
            }

            // Cambia el texto del Canvas
            if (canvasText != null)
            {
                canvasText.text = isParticlesActive ? secondText : initialText;
            }
        }
    }
}



