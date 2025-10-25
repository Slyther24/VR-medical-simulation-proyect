using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class CameraHUDController : MonoBehaviour
{
    public Transform emptyPosition; // Posición de referencia para la cámara de fotos
    public GameObject cameraObject; // Cámara 3D que el jugador puede tomar
    public Camera photoCamera; // Cámara que simula el visor de la cámara de fotos

    public GameObject hudCanvas; // HUD de la cámara
    public Image hudIndicator; // Imagen en el HUD que cambia de color
    public Image flashEffect; // Imagen para el efecto de flash
    public AudioSource flashSound;
    public AudioSource muerteF; // Sonido del flash
    public LayerMask ghostLayer; // Capa de los fantasmas
    public XRGrabInteractable grabInteract2; // Componente para detectar el evento de activación
    public float photoCooldown = 2.0f; // Tiempo de espera entre fotos en segundos

    private bool hudActive = false;
    private bool isOnCooldown = false; // Estado de cooldown
    private RaycastHit currentGhost; // Para almacenar el fantasma detectado

    private void Start()
    {
        // Asegurarse de que la cámara de fotos y el HUD estén desactivados al inicio
        photoCamera.gameObject.SetActive(false);
        hudCanvas.SetActive(false);
        hudIndicator.color = Color.white; // Inicia con el indicador en rojo
        flashEffect.color = new Color(1, 1, 1, 0); // Asegura que el flash esté invisible al inicio

        // Agregar el listener al evento activated de grabInteract2
        grabInteract2.activated.AddListener(_ => TryCapturePhoto());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sphere"))
        {
            Debug.Log("COLLISION WITH SPHERE ENTER");
            ActivatePhotoMode();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        DeactivatePhotoMode();
    }

    private void Update()
    {
        if (hudActive)
        {
            // Lanza un rayo constante para detectar fantasmas
            DetectGhost();
        }
    }

    void ActivatePhotoMode()
    {
        // Sincroniza la posición y rotación de la photoCamera con la posición de referencia
        photoCamera.transform.position = emptyPosition.position;
        photoCamera.transform.rotation = emptyPosition.rotation;

        // Activa la cámara de fotos y el HUD
        photoCamera.gameObject.SetActive(true);
        hudCanvas.SetActive(true);
        hudActive = true;
    }

    void DeactivatePhotoMode()
    {
        // Solo desactiva el HUD y la cámara de fotos, manteniendo la cámara VR activa
        photoCamera.gameObject.SetActive(false);
        hudCanvas.SetActive(false);
        hudActive = false;
        hudIndicator.color = Color.white; // Resetea el color del indicador
        flashEffect.color = new Color(1, 1, 1, 0); // Asegura que el flash esté invisible
        Debug.Log("COLLISION EXIT");
    }

    void DetectGhost()
    {
        Ray ray = new Ray(photoCamera.transform.position, photoCamera.transform.forward);

        // Lanza el rayo y verifica si golpea un fantasma
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ghostLayer))
        {
            currentGhost = hit; // Guarda el fantasma detectado
            hudIndicator.color = Color.green; // Cambia el color del indicador a verde
        }
        else
        {
            currentGhost = default; // No hay fantasma detectado
            hudIndicator.color = Color.white; // Cambia el color del indicador a rojo
        }
    }

    void TryCapturePhoto()
    {
        // Verificar si no está en cooldown antes de capturar la foto
        if (!isOnCooldown)
        {
            CapturePhoto();
        }
    }

    void CapturePhoto()
    {
        // Ejecuta el efecto de flash y sonido
        StartCoroutine(FlashEffectCoroutine());

        // Verifica si hay un fantasma en el visor
        if (currentGhost.collider != null)
        {
            Debug.Log("Fantasma capturado: " + currentGhost.collider.gameObject.name);
            Destroy(currentGhost.collider.gameObject); // Destruye el fantasma detectado
        }
        else
        {
            Debug.Log("No se detectó ningún fantasma en el visor.");
        }

        // Inicia el cooldown para evitar que se tomen fotos seguidas
        StartCoroutine(PhotoCooldownCoroutine());
    }

    System.Collections.IEnumerator FlashEffectCoroutine()
    {
        // Reproduce el sonido del flash
        flashSound.Play();
        muerteF.Play();

        // Activa el flash, haciendo que la imagen cubra la pantalla brevemente
        flashEffect.color = new Color(1, 1, 1, 1); // Blanco total

        // Espera un instante
        yield return new WaitForSeconds(0.1f);

        // Gradualmente desvanece el flash
        for (float t = 1f; t > 0; t -= Time.deltaTime * 5)
        {
            flashEffect.color = new Color(1, 1, 1, t);
            yield return null;
        }

        // Asegura que el flash esté completamente invisible al final
        flashEffect.color = new Color(1, 1, 1, 0);
    }

    System.Collections.IEnumerator PhotoCooldownCoroutine()
    {
        // Activa el estado de cooldown
        isOnCooldown = true;
        yield return new WaitForSeconds(photoCooldown);
        // Desactiva el estado de cooldown
        isOnCooldown = false;
    }
}





