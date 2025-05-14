using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sentarse : MonoBehaviour
{
    public Transform posicionSentado; 

    //Transición
    public float tiempoTransicion = 0.5f; 

    private bool estaSentado = false;
    private bool estaEnTransicion = false;

    private Vector3 posicionOriginal;
    private Quaternion rotacionOriginal;

    private Vector3 posicionInicioTransicion;
    private Quaternion rotacionInicioTransicion;
    private Vector3 posicionDestinoTransicion;
    private Quaternion rotacionDestinoTransicion;
    private float tiempoInicioTransicion = 0f;

    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody no encontrado");
        }
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        float distanciaRayo = 3f;
        Debug.DrawRay(ray.origin, ray.direction * distanciaRayo, Color.green);

        if (Physics.Raycast(ray, out RaycastHit hit, distanciaRayo))
        {
            if (hit.collider.CompareTag("Silla"))
            {
                posicionSentado = hit.collider.transform.Find("posicionSentado");

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (!estaSentado && !estaEnTransicion)
                    {
                        Sentar();
                    }
                }
            }
        }

        //Levantarse
        if (estaSentado && !estaEnTransicion &&
            (Input.GetKeyDown(KeyCode.W) ||
             Input.GetKeyDown(KeyCode.A) ||
             Input.GetKeyDown(KeyCode.S) ||
             Input.GetKeyDown(KeyCode.D)))
        {
            Levantarse();
        }

        //Transición
        if (estaEnTransicion)
        {
            float tiempoTranscurrido = Time.time - tiempoInicioTransicion;
            float t = Mathf.Clamp01(tiempoTranscurrido / tiempoTransicion);

            transform.position = Vector3.Lerp(posicionInicioTransicion, posicionDestinoTransicion, t);
            transform.rotation = Quaternion.Slerp(rotacionInicioTransicion, rotacionDestinoTransicion, t);

            if (t >= 1f)
            {
                estaEnTransicion = false;

                if (!estaSentado && rb != null)
                {
                    rb.isKinematic = false;
                    rb.useGravity = true; 
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
            }
        }
    }

    void Sentar()
    {
        if (posicionSentado != null && !estaEnTransicion)
        {
            estaSentado = true;
            estaEnTransicion = true;

            posicionOriginal = transform.position;
            rotacionOriginal = transform.rotation;

            posicionInicioTransicion = transform.position;
            rotacionInicioTransicion = transform.rotation;

            Vector3 posicionAjustada = posicionSentado.position + new Vector3(0, -1.3f, 0);

            posicionDestinoTransicion = posicionAjustada;
            rotacionDestinoTransicion = posicionSentado.rotation;

            tiempoInicioTransicion = Time.time;

            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true;
            }

            if (capsuleCollider != null)
            {
                capsuleCollider.enabled = false;
            }
        }
    }

    void Levantarse()
    {
        if (!estaEnTransicion)
        {
            estaSentado = false;
            estaEnTransicion = true;

            posicionInicioTransicion = transform.position;
            rotacionInicioTransicion = transform.rotation;

            posicionDestinoTransicion = posicionOriginal;
            rotacionDestinoTransicion = rotacionOriginal;

            tiempoInicioTransicion = Time.time;

            if (rb != null)
            {
                rb.isKinematic = true;
            }

             if (capsuleCollider != null)
            {
                capsuleCollider.enabled = true; 
            }
        }
    }
}