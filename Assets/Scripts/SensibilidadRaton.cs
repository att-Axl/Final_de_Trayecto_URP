using System.Collections;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensibilidadRaton : MonoBehaviour
{

    private float sensibilidad = 100f;
    private Transform playerBody;

    private float currentSensibilidad;

    void Start()
    {
        currentSensibilidad = PlayerPrefs.GetFloat("SensibilidadRaton", sensibilidad);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * currentSensibilidad * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * currentSensibilidad * Time.deltaTime;

        transform.localRotation *= Quaternion.Euler(-mouseY, 0f, 0f);

        if (playerBody != null)
        {
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }

    public void AjustarSensibilidad(float valor)
    {
        currentSensibilidad = valor;
        PlayerPrefs.SetFloat("SensibilidadRaton", valor);
        PlayerPrefs.Save();
    }
}