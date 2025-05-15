using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractNotas : MonoBehaviour
{
    public GameObject textCanvas;           
    public TextMeshProUGUI textoNotaUno;    
    public float distanciaMaxima = 3f;      


    private bool mostrandoContenido = false;
    private GameObject notaActual = null;

    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        Debug.DrawRay(ray.origin, ray.direction * distanciaMaxima, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, distanciaMaxima))
        {
            if (hit.collider.CompareTag("Nota"))
            {
                notaActual = hit.collider.gameObject;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    mostrandoContenido = !mostrandoContenido;

                    if (textCanvas != null)
                        textCanvas.SetActive(mostrandoContenido);
                    Time.timeScale = mostrandoContenido ? 0f : 1f;
                }
            }
            else
            {
                notaActual = null;
            }
        }
        else
        {
            notaActual = null;
        }

        if (notaActual == null && textCanvas != null && textCanvas.activeSelf)
        {
            textCanvas.SetActive(false);
            mostrandoContenido = false;
            Time.timeScale = 1f;
        }
    }
}
