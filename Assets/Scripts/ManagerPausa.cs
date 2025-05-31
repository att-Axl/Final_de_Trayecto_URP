using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerPausa : MonoBehaviour
{
    public static ManagerPausa Instance { get; private set; }

    public GameObject canvasAjustesPrefab;

    private GameObject menuInstance;
    private bool estaPausado = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AlternarPausa();
        }
    }

    public void AlternarPausa()
    {
        estaPausado = !estaPausado;

        if (estaPausado)
        {
            MostrarMenuDePausa();
        }
        else
        {
            OcultarMenuDePausa();
        }
    }

    private void MostrarMenuDePausa()
    {
    
        if (menuInstance != null)
        {
            Destroy(menuInstance);
        }

        if (canvasAjustesPrefab != null)
        {
            menuInstance = Instantiate(canvasAjustesPrefab);
            Time.timeScale = 0f;
            MostrarRaton(); 
        }
    }

    private void OcultarMenuDePausa()
    {
        if (menuInstance != null)
        {
            Destroy(menuInstance);
            ReanudarJuego();
        }
    }


    public void ContinuarJuego()
    {
        estaPausado = false;
        OcultarMenuDePausa();
    }


    public void MostrarRaton()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void ReanudarJuego()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}