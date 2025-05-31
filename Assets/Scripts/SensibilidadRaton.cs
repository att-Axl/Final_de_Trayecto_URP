using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensibilidadRaton : MonoBehaviour
{
    private float defaultSensitivity = 100f;
    private Transform playerBody;

    private float currentSensitivity;

    void Start()
    {
        currentSensitivity = PlayerPrefs.GetFloat("SensibilidadRaton", defaultSensitivity);
        
        //slider.value = currentSensitivity;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * currentSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * currentSensitivity * Time.deltaTime;

        transform.localRotation *= Quaternion.Euler(-mouseY, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    public void SetSensitivity(float value)
    {
        currentSensitivity = value;
        PlayerPrefs.SetFloat("SensibilidadRaton", value); 
        PlayerPrefs.Save(); 
    }
}