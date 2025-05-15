using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NuevaCarta", menuName = "Cartas/Carta")]

public class CartaData : ScriptableObject
{
    public Sprite imagenCarta;
    
    [TextArea] public string mensaje;
}