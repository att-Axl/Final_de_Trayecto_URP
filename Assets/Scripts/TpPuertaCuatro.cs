using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TpPuertaCuatro : MonoBehaviour
{

    public string nomEscena = "VagonCinco";
    public float distanciaMaxima = 2f;
    public TextMeshProUGUI textoE;

    private GameObject puertaActual = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        Debug.DrawRay(ray.origin, ray.direction * distanciaMaxima, Color.red);

            if(Physics.Raycast(ray, out RaycastHit hit, distanciaMaxima)){
                if(hit.collider.CompareTag("Puerta")){
                    puertaActual = hit.collider.gameObject;

                    //Debug.Log("Mirando puerta");

                if (textoE != null)
                    textoE.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    SceneManager.LoadScene(nomEscena);
                }
            }
            else
            {
                if (textoE != null)
                    textoE.gameObject.SetActive(false);
                puertaActual = null;
            }
        }
        else
        {
            if (textoE != null)
                textoE.gameObject.SetActive(false);
            puertaActual = null;
        }
    }
}

