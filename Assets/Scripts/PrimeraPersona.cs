using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimeraPersona : MonoBehaviour
{
    // Movimiento
    public float velocidadMovimiento = 5f;
    public float velocidadCorrer = 10f; 
    public float gravedadExtra = -9.81f;

    public float velocidadAgachado = 2.5f;

    private bool isGrounded;
    
    public Transform controlDeSuelo;
    public float alturaDelPie = 0.2f;

    // Agacharse
    public float alturaNormal = 1f;      
    public float alturaAgachado = 0.4f;   
    private bool estaAgachado = false;  

    // Transición
    public float velocidadTransicion = 5f; 
    private float alturaDeseada;
   

    // Mirada
    public float sensibilidadMouse = 200f;
    public Transform cameraHolder;

    private Camera cam;
    private Rigidbody rb;

    private Vector3 groundForward;

    // Cámara
    public float alturaCamaraNormal = 1f;
    public float alturaCamaraAgachado = 0.5f;

    private float alturaCamaraInicial;

    private Salto scriptSalto;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb.freezeRotation = true;

        alturaCamaraInicial = cameraHolder.localPosition.y;
        alturaDeseada = alturaNormal;

        scriptSalto = GetComponent<Salto>();
    }

    void Update()
    {
        Mirada();
        
        //DetectarSuelo();



        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            Agacharse(true);
        }
        else
        {
            Agacharse(false);
        }
    }

    void FixedUpdate()
    {
        Movimiento();
        AplicarGravedad();

            
        CapsuleCollider col = GetComponent<CapsuleCollider>();
        if (col != null)
        {

            /*
            // Altura collider
            float currentHeight = col.height;
            float targetHeight = estaAgachado ? alturaAgachado : alturaNormal;
            col.height = Mathf.Lerp(currentHeight, targetHeight, Time.fixedDeltaTime * velocidadTransicion);

            // Centro collider
            Vector3 currentCenter = col.center;
            Vector3 targetCenter = new Vector3(0, col.height / 2, 0);
            col.center = Vector3.Lerp(currentCenter, targetCenter, Time.fixedDeltaTime * velocidadTransicion);
       */
        }

        // Altura cámara 
        float targetCamHeight = estaAgachado ? alturaCamaraAgachado : alturaCamaraNormal;

        Vector3 posCam = cameraHolder.localPosition;
        posCam.y = Mathf.Lerp(posCam.y, targetCamHeight, Time.fixedDeltaTime * velocidadTransicion);
        cameraHolder.localPosition = posCam;
    }

    void Movimiento()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(x, 0f, z).normalized;

        if (move.magnitude >= 0.1f)
        {
            Vector3 forward = Vector3.ProjectOnPlane(cam.transform.forward, Vector3.up);
            forward.Normalize();

            Vector3 right = Vector3.ProjectOnPlane(cam.transform.right, Vector3.up);
            right.Normalize();

            Vector3 direction = forward * z + right * x;

            //Correr
            float velocidadActual = velocidadMovimiento;

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                velocidadActual = velocidadCorrer;
            }

            if (estaAgachado)
            {
                velocidadActual = velocidadAgachado;
            }

            rb.velocity = direction * velocidadActual + new Vector3(0f, rb.velocity.y, 0f);
        }
        else
        {
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
        }
    }

    void AplicarGravedad()
    {
        if (!isGrounded)
        {
            rb.velocity += Vector3.up * gravedadExtra * Time.fixedDeltaTime;
        }
    }


    void Mirada()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadMouse * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadMouse * Time.deltaTime;

        // Rotación horizontal 
        transform.Rotate(Vector3.up, mouseX);

        // Rotación vertical 
        Vector3 currentRot = cameraHolder.localEulerAngles;
        currentRot.x -= mouseY;

        if (currentRot.x > 180)
            currentRot.x -= 360;

        currentRot.x = Mathf.Clamp(currentRot.x, -80, 80);

        cameraHolder.localEulerAngles = currentRot;
    }

    void Agacharse(bool agachado)
    {
        estaAgachado = agachado;

        CapsuleCollider col = GetComponent<CapsuleCollider>();
        /*
        if (col != null)
        {
            float alturaFinal = agachado ? alturaAgachado : alturaNormal;
            Vector3 centro = col.center;
            centro.y = alturaFinal / 2;
            col.height = alturaFinal;
            col.center = centro;
        }
        */
    }
}