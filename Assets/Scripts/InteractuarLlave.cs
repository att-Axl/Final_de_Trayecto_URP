using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractuarLlave : MonoBehaviour
{
    public GameObject textCanvas;          
    public TextMeshProUGUI textoInfoLLave;  
    public float distanciaMaxima = 3f;    

    private bool mostrandoContenido = false;
    private GameObject LlaveActual = null;
    public static bool tieneLlave = false;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        Debug.DrawRay(ray.origin, ray.direction * distanciaMaxima, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, distanciaMaxima))
        {
            if (hit.collider.CompareTag("Llave"))
            {
                LlaveActual = hit.collider.gameObject;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    mostrandoContenido = !mostrandoContenido;

                    if (textCanvas != null)
                        textCanvas.SetActive(mostrandoContenido);

                    tieneLlave = true;

                    Time.timeScale = mostrandoContenido ? 0f : 1f;
                    AudioManager.Instance.SonarClipUnaVez(AudioManager.Instance.Llaves);

                    if (!mostrandoContenido && LlaveActual != null)
                    {
                        Destroy(LlaveActual);
                    }
                }
            }
            else
            {
                LlaveActual = null;
            }
        }
        else
        {
            LlaveActual = null;
        }

        if (LlaveActual == null && textCanvas != null && textCanvas.activeSelf)
        {
            textCanvas.SetActive(false);
            mostrandoContenido = false;
            Time.timeScale = 1f;
        }
    }
}