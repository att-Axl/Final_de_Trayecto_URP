using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractuarPuerta : MonoBehaviour
{
    public float distanciaMaxima = 5f;
    public GameObject mensajeNecesitasLlave;
    public Transform Door_A;

    private Animator animator;
    private bool PuertaDingus = false;


    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator no encontrado en Door_A");
        }
    }

    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * distanciaMaxima, Color.green);


        if (Physics.Raycast(ray, out hit, distanciaMaxima))
        {

            if (hit.collider.gameObject == this.gameObject)
            {

                if (!InteractuarLlave.tieneLlave && mensajeNecesitasLlave != null)
                    mensajeNecesitasLlave.SetActive(true);


                if (Input.GetKeyDown(KeyCode.E))
                {
                    AudioManager.Instance.SonarClipUnaVez(AudioManager.Instance.Puerta);
                    if (InteractuarLlave.tieneLlave)
                    {
                        Debug.Log("Tienes llave. Puerta abierta.");
                        if (!PuertaDingus && animator != null)
                        {
                            animator.SetBool("PuertaDingus", true);
                            PuertaDingus = true;
                            AudioManager.Instance.SonarClipUnaVez(AudioManager.Instance.Puerta);
                            AudioManager.Instance.SonarClipUnaVez(AudioManager.Instance.Gato);
                        }
                    }
                    else
                    {
                       Debug.Log("No tienes llave");
                        if (mensajeNecesitasLlave != null)
                        {
                            Debug.Log("Mostrando mensaje: Necesitas llave");
                            mensajeNecesitasLlave.SetActive(true);
                        }
                    }
                }
            }
            else
            {
              
                if (mensajeNecesitasLlave != null && mensajeNecesitasLlave.activeSelf)
                {
                    mensajeNecesitasLlave.SetActive(false);
                }
            }
        }
        else
        {
      
            if (mensajeNecesitasLlave != null && mensajeNecesitasLlave.activeSelf)
            {
                mensajeNecesitasLlave.SetActive(false);
            }
        }
    }
}