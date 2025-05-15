using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractNotas : MonoBehaviour
{
    public GameObject textCanvas;           // Canvas que contiene el texto de la nota
    public TextMeshProUGUI textoNotaUno;    // Texto dentro del canvas (opcional)
    public float distanciaMaxima = 3f;      // Distancia máxima de interacción

    private bool mostrandoContenido = false;
    private GameObject notaActual = null;

    void Update()
    {
        // Raycast desde la cámara (mirada del jugador)
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        Debug.DrawRay(ray.origin, ray.direction * distanciaMaxima, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, distanciaMaxima))
        {
            if (hit.collider.CompareTag("Nota"))
            {
                notaActual = hit.collider.gameObject;

                // Presionar E para mostrar/ocultar el canvas y pausar/reanudar
                if (Input.GetKeyDown(KeyCode.E))
                {
                    mostrandoContenido = !mostrandoContenido;

                    if (textCanvas != null)
                        textCanvas.SetActive(mostrandoContenido);

                    // Pausar o reanudar el juego
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

        // Si deja de mirar la nota, ocultar automáticamente
        if (notaActual == null && textCanvas != null && textCanvas.activeSelf)
        {
            textCanvas.SetActive(false);
            mostrandoContenido = false;
            Time.timeScale = 1f;
        }
    }
}