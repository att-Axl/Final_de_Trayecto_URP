using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salto : MonoBehaviour
{
    public float fuerzaDeSalto = 5f;

    private Rigidbody rb;

    private bool isGrounded; 

    public LayerMask queEsSuelo;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ActualizarEstadoSuelo(bool estaEnSuelo)
    {
        //isGrounded = estaEnSuelo;
    }

public void Update(){
        if (Input.GetButtonDown("Jump"))
        {
      
            RealizarSalto();
        }
}

    public void RealizarSalto()
    {
        isGrounded = DetectarSuelo();
        if(isGrounded == false) return;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(Vector3.up * fuerzaDeSalto, ForceMode.VelocityChange);
        
    }

    bool DetectarSuelo()
    {
        bool isG = false;
        Debug.DrawRay(transform.position, Vector3.down, Color.green, 1f);
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1f, queEsSuelo))
        {
            isG = true;
        }
        else
        {
            isG = false;
        }
        return isG;
    
    }
}