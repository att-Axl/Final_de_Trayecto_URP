using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DialogoManager : MonoBehaviour
{
    public static DialogoManager Instance;

    public GameObject panelDialogo;
    public TextMeshProUGUI textoDialogo;
    public Image iconoContinuar;

    public float velocidadEscritura = 0.05f;
    public KeyCode teclaContinuar = KeyCode.Mouse0;

    private string[] lineasActuales;
    private int indiceLineaActual;
    private Transform objetivoEnfoque;

    private bool esperandoContinuar = false;

    private Transform camaraJugador;
    private Vector3 rotacionOriginalCamara;

    private Rigidbody rbJugador;
    private PrimeraPersona movimientoJugador;
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        camaraJugador = Camera.main.transform;
        panelDialogo.SetActive(false);

        GameObject jugador = GameObject.FindGameObjectWithTag("Player");
        if (jugador != null)
        {
            rbJugador = jugador.GetComponent<Rigidbody>();
            movimientoJugador = jugador.GetComponent<PrimeraPersona>();
        }
    }

    public void IniciarDialogo(string[] lineas, Transform objetivo)
    {
        lineasActuales = lineas;
        indiceLineaActual = 0;
        objetivoEnfoque = objetivo;

        panelDialogo.SetActive(true);
        textoDialogo.text = "";
        iconoContinuar.enabled = false;

        rotacionOriginalCamara = camaraJugador.eulerAngles;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // //Bloquea el movimiento
        // if (rbJugador != null)
        // {
        //     rbJugador.velocity = Vector3.zero;
        //     rbJugador.constraints = RigidbodyConstraints.FreezePositionX |
        //                            RigidbodyConstraints.FreezePositionY |
        //                            RigidbodyConstraints.FreezePositionZ |
        //                            RigidbodyConstraints.FreezeRotationX |
        //                            RigidbodyConstraints.FreezeRotationY |
        //                            RigidbodyConstraints.FreezeRotationZ;
        // }

        // if (movimientoJugador != null)
        // {
        //     movimientoJugador.BloquearControles();
        // }

        StartCoroutine(EnfocarObjetivo());
        SiguienteLinea();
    }

    private IEnumerator EnfocarObjetivo()
    {
        float duracion = 0.5f;
        float tiempoTranscurrido = 0f;

        Quaternion rotInicio = camaraJugador.rotation;
        Quaternion rotFin = Quaternion.LookRotation(objetivoEnfoque.position - camaraJugador.position);

        while (tiempoTranscurrido < duracion)
        {
            camaraJugador.rotation = Quaternion.Slerp(rotInicio, rotFin, tiempoTranscurrido / duracion);
            tiempoTranscurrido += Time.deltaTime;
            yield return null;
        }

        camaraJugador.rotation = rotFin;
    }

    private void SiguienteLinea()
    {
       
        if (indiceLineaActual >= lineasActuales.Length)
        {
            FinalizarDialogo();
            return;
        }
        AudioManager.Instance.ReproducirAudioPaloma();

        StopAllCoroutines();
        StartCoroutine(MostrarTexto(lineasActuales[indiceLineaActual]));
        indiceLineaActual++;
        esperandoContinuar = false;
    }

    private IEnumerator MostrarTexto(string linea)
    {
        textoDialogo.text = "";
        float velocidadActual = velocidadEscritura;

       foreach (char c in linea)
        {
            textoDialogo.text += c;
            textoDialogo.ForceMeshUpdate();

            //Si se mantiene click, acelera el texto
            if (Input.GetKey(teclaContinuar))
            {
                velocidadActual = Mathf.Max(0.01f, velocidadEscritura - 0.025f);
            }
            else
            {
                velocidadActual = velocidadEscritura;
            }

            yield return new WaitForSecondsRealtime(velocidadActual);
        }

        iconoContinuar.enabled = true;
        esperandoContinuar = true;
    }

    private void Update()
    {
        if (!EstaEnDialogo || !esperandoContinuar) return;

        if (Input.GetKeyDown(teclaContinuar))
        {
            SiguienteLinea();
        }
    }

    private void FinalizarDialogo()
    {
        AudioManager.Instance.SonarClipUnaVez(AudioManager.Instance.PuertaMetro);
        panelDialogo.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        camaraJugador.eulerAngles = rotacionOriginalCamara;

        // //Desbloquea el movimiento
        // if (rbJugador != null)
        // {
        //     rbJugador.velocity = Vector3.zero;
        //     rbJugador.constraints = RigidbodyConstraints.None;
        // }

        // if (movimientoJugador != null)
        // {
        //     movimientoJugador.DesbloquearControles();
        // }

    }

    private bool EstaEnDialogo => Instance != null && Instance.panelDialogo.activeSelf;
}