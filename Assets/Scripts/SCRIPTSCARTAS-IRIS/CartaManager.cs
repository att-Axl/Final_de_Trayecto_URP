using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CartaManager : MonoBehaviour
{
   public static CartaManager Instance;

    public GameObject cartaUIPanel;
    public Image cartaImagen;
    public TextMeshProUGUI cartaTexto;
    public TextMeshProUGUI cartaIndicacionTexto;

    private bool cartaActiva = false;

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

        cartaUIPanel.SetActive(false);
    }

    public void MostrarCarta(CartaData carta)
{
    cartaUIPanel.SetActive(true); 
    cartaImagen.sprite = carta.imagenCarta;
    cartaTexto.text = carta.mensaje;
    cartaIndicacionTexto.text = "Pulsa E para cerrar";
    Time.timeScale = 0f;
}


    public void CerrarCarta()
    {
        cartaUIPanel.SetActive(false);
        cartaActiva = false;

        // Reanuda el juego (si pausaste)
        Time.timeScale = 1f;
    }

    public bool EstaActiva()
    {
        return cartaActiva;
    }
}