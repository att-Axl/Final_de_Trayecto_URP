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

        StopAllCoroutines();
        StartCoroutine(MostrarTexto(lineasActuales[indiceLineaActual]));
        indiceLineaActual++;
        esperandoContinuar = false;
    }

    private IEnumerator MostrarTexto(string linea)
    {
        textoDialogo.text = "";

        foreach (char c in linea)
        {
            textoDialogo.text += c;
            yield return new WaitForSeconds(velocidadEscritura);
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
        panelDialogo.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        camaraJugador.eulerAngles = rotacionOriginalCamara;
    }

    private bool EstaEnDialogo => Instance != null && Instance.panelDialogo.activeSelf;
}