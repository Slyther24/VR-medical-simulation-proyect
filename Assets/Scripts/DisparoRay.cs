using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoRay : MonoBehaviour
{
    public Transform firePoint;
    public int damage = 25;

    public LineRenderer lineRenderer;
    
    public void Disparar()
    {
        StartCoroutine(Disparo());
    }

    IEnumerator Disparo()
    {
        RaycastHit hit;
        bool hitInfo = Physics.Raycast(firePoint.position, firePoint.forward, out hit, 50f);
        if (hitInfo)
        {
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            lineRenderer.SetPosition(0,firePoint.position); 
            lineRenderer.SetPosition(1,firePoint.position + firePoint.forward * 20); 
        }

        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.05f);
        lineRenderer.enabled = false;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
