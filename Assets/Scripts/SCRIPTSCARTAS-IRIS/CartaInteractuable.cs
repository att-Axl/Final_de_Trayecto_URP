using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CartaInteractuable : MonoBehaviour
{
    public CartaData datosCarta;

    public void Mostrar()
    {
        CartaManager.Instance.MostrarCarta(datosCarta);
    }
}