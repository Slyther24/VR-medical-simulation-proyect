using UnityEngine;

public class CanvasTimer : MonoBehaviour
{
    public Canvas canvasToHide; // Referencia al Canvas que deseas ocultar
    public float displayTime = 10f; // Tiempo en segundos que el Canvas estará activo

    private void Start()
    {
        // Asegurarse de que el Canvas esté activo al inicio
        if (canvasToHide != null)
        {
            canvasToHide.gameObject.SetActive(true);
            // Iniciar la corrutina para ocultar el Canvas después del tiempo especificado
            StartCoroutine(HideCanvasAfterTime());
        }
        else
        {
            Debug.LogWarning("Canvas no asignado en el inspector.");
        }
    }

    private System.Collections.IEnumerator HideCanvasAfterTime()
    {
        // Esperar el tiempo especificado
        yield return new WaitForSeconds(displayTime);
        // Desactivar el Canvas
        canvasToHide.gameObject.SetActive(false);
    }
}

