using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractNotas : MonoBehaviour
{
    public GameObject textCanvas;

    public TextMeshProUGUI textoNotaUno;

    private GameObject nota = null;

    public float distanciaMaxima = 2f;

    void Start()
    {
        if (textCanvas != null)
            textCanvas.SetActive(false);

        if (textoNotaUno != null)
            textoNotaUno.gameObject.SetActive(false);

    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        float distanciaRayo = 3f;
        Debug.DrawRay(ray.origin, ray.direction * distanciaRayo, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, distanciaRayo))
        {
            if (hit.collider.CompareTag("Nota"))
            {
                nota = hit.collider.gameObject;

                Debug.Log("Mirando nota");

                if (textoNotaUno != null)
                    textoNotaUno.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                //    textCanvas.SetActive(!textCanvas.activeSelf);
                   textoNotaUno.gameObject.SetActive(true);
                }
            else
            {
                nota = null;
                if (textoNotaUno != null)
                    textoNotaUno.gameObject.SetActive(false);
            }
        }
        else
        {
            nota = null;
            if (textoNotaUno != null)
                textoNotaUno.gameObject.SetActive(false);
        }
    }
    }
}