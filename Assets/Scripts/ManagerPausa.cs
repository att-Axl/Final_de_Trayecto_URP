using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerPausa: MonoBehaviour
{
    public static ManagerPausa Instance { get; private set; }

    private GameObject menuDePausaPrefab; 
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
        if (menuDePausaPrefab != null && menuInstance == null)
        {
            menuInstance = Instantiate(menuDePausaPrefab);
            Time.timeScale = 0f; 
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void OcultarMenuDePausa()
    {
        if (menuInstance != null)
        {
            Destroy(menuInstance);
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void ContinuarJuego()
    {
        estaPausado = false;
        OcultarMenuDePausa();
    }
}