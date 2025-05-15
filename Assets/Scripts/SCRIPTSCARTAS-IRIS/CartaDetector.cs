using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartaDetector : MonoBehaviour
{
    public float distancia = 3f;
    public LayerMask capaCarta;

    private void Update()
    {
        // Si se pulsa E
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Lanzamos un rayo hacia adelante desde la posición del jugador/cámara
            Ray ray = new Ray(transform.position, transform.forward);
            Debug.DrawRay(transform.position, transform.forward * distancia, Color.green, 1f);  // Esto dibuja el raycast en la escena

            if (Physics.Raycast(ray, out RaycastHit hit, distancia, capaCarta))
            {
                Debug.Log("Raycast ha tocado: " + hit.collider.name); // Ver qué colisiona el raycast

                // Si el raycast toca una carta
                CartaInteractuable carta = hit.collider.GetComponent<CartaInteractuable>();
                if (carta != null)
                {
                    carta.Mostrar();  // Muestra la carta en la UI
                }
            }
        }
    }
}